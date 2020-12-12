using AutoMapper;
using Expenses.Web.Business.Data;
using Expenses.Web.Business.Extensions;
using Expenses.Web.Business.Mediator.Account;
using Expenses.Web.Business.Mediator.Categories;
using Expenses.Web.Business.Mediator.Transactions;
using Expenses.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Controllers
{
  [Authorize]
  public class TransactionController : Controller
  {
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public TransactionController(IMediator mediator, IMapper mapper)
    {
      this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> List(int page = 0, Guid? categoryId = null, CancellationToken cancellationToken = default)
    {
      var userId = this.GetUserId();

      var categories = await mediator.Send(new CategoryListRequest(userId), cancellationToken);
      ViewBag.Categories = categories;
      ViewBag.CategoryId = categoryId.HasValue ? $"{categoryId.Value}" : string.Empty;

      var transactions = await mediator.Send(
        new TransactionListRequest(userId)
        {
          Page = page,
          CategoryId = categoryId
        },
      cancellationToken);

      ViewBag.Count = transactions.Count;
      ViewBag.Pages = transactions.Pages;
      ViewBag.Index = transactions.Index;

      return View(transactions.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid? id, CancellationToken cancellationToken)
    {
      var userId = this.GetUserId();

      TransactionViewModel model = null;

      if (id.HasValue)
      {
        model = await mediator.Send(new TransactionRequest(id.Value, userId), cancellationToken);
      }

      if (model == null)
      {
        var type = this.GetRequestValue("type");

        model = new TransactionViewModel
        {
          Type = type
        };
      }

      await CreateViewDataAsync(userId, cancellationToken);

      ViewBag.ReturnUrl = this.GetReturnUrl();

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TransactionViewModel model, CancellationToken cancellationToken)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      var userId = this.GetUserId();
      var returnUrl = this.GetReturnUrl();

      if (ModelState.IsValid)
      {
        var request = mapper.Map<TransactionEditRequest>(model);
        request.UserId = userId;

        await mediator.Send(request, cancellationToken);

        if (string.IsNullOrEmpty(returnUrl))
        {
          return RedirectToAction(nameof(List));
        }
        else
        {
          if (Url.IsLocalUrl(returnUrl))
          {
            return Redirect(returnUrl);
          }
          else
          {
            throw new InvalidOperationException("Boo !");
          }
        }
      }

      await CreateViewDataAsync(userId, cancellationToken);

      return View(model);
    }

    private async Task CreateViewDataAsync(Guid userId, CancellationToken cancellationToken)
    {
      var accounts = await mediator.Send(new AccountListRequest(userId), cancellationToken);
      ViewData.Add("accounts", accounts);

      var categories = await mediator.Send(new CategoryListRequest(userId), cancellationToken);
      ViewData.Add("categories", categories);

      var types = Enum.GetNames(typeof(TransactionType)).ToList();
      ViewData.Add("types", types);
    }
  }
}
