using AutoMapper;
using Expenses.Web.Business.Extensions;
using Expenses.Web.Business.Mediator.Categories;
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
  public class CategoryController : Controller
  {
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public CategoryController(IMediator mediator, IMapper mapper)
    {
      this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
      var userId = this.GetUserId();
      var model = await mediator.Send(new CategoryListRequest(userId), cancellationToken);

      return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid? id, CancellationToken cancellationToken)
    {
      CategoryViewModel model = null;

      if (id.HasValue)
      {
        var userId = this.GetUserId();
        model = await mediator.Send(new CategoryRequest(id.Value, userId), cancellationToken);
      }

      if (model == null)
      {
        model = new CategoryViewModel
        {
        };
      }

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryViewModel model, CancellationToken cancellationToken)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      if (ModelState.IsValid)
      {
        var userId = this.GetUserId();

        var request = mapper.Map<CategoryEditRequest>(model);
        request.UserId = userId;

        await mediator.Send(request, cancellationToken);

        return RedirectToAction(nameof(List));
      }

      return View(model);
    }
  }
}
