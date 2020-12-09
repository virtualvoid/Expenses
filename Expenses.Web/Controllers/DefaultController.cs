using Expenses.Web.Business.Extensions;
using Expenses.Web.Business.Mediator.Default;
using Expenses.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Controllers
{
  [Authorize]
  public class DefaultController : Controller
  {
    private readonly IMediator mediator;

    public DefaultController(IMediator mediator)
    {
      this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
      var userId = this.GetUserId();

      var overviewByCategory = await mediator.Send(new OverviewByCategoryRequest(userId), cancellationToken);
      ViewBag.OverviewByCategory = overviewByCategory;

      return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
