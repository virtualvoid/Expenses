using AutoMapper;
using Expenses.Web.Business.Extensions;
using Expenses.Web.Business.Mediator.Account;
using Expenses.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public AccountController(IMediator mediator, IMapper mapper)
    {
      this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
      var userId = this.GetUserId();
      var model = await mediator.Send(new AccountListRequest(userId), cancellationToken);

      return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid? id, CancellationToken cancellationToken)
    {
      AccountViewModel model = null;

      if (id.HasValue)
      {
        var userId = this.GetUserId();
        model = await mediator.Send(new AccountRequest(id.Value, userId), cancellationToken);
      }

      if (model == null)
      {
        model = new AccountViewModel
        {
        };
      }

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AccountViewModel model, CancellationToken cancellationToken)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      if (ModelState.IsValid)
      {
        var userId = this.GetUserId();

        var request = mapper.Map<AccountEditRequest>(model);
        request.UserId = userId;

        await mediator.Send(request, cancellationToken);

        return RedirectToAction(nameof(List));
      }

      return View(model);
    }
  }
}
