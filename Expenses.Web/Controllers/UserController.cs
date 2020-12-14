using AutoMapper;
using Expenses.Web.Business.Mediator.User;
using Expenses.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Controllers
{
  [Authorize]
  public class UserController : Controller
  {
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
      this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    #region Registration
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
      return View(new RegistrationViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegistrationViewModel model, CancellationToken cancellationToken)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      if (ModelState.IsValid)
      {
        var request = mapper.Map<RegistrationRequest>(model);
        var result = await mediator.Send(request, cancellationToken);
        if (result == null)
        {
          throw new InvalidOperationException();
        }

        if (result.Success)
        {
          return RedirectToAction(nameof(Login), new { userName = model.UserName });
        }
        else
        {
          ModelState.AddModelError("", result.Message);
        }
      }

      return View(model);
    }
    #endregion

    #region Login

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string userName = "", string returnUrl = "")
    {
      var model = new LoginViewModel
      {
        UserName = userName,
        ReturnUrl = returnUrl
      };

      return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      if (ModelState.IsValid)
      {
        var request = mapper.Map<LoginRequest>(model);
        var user = await mediator.Send(request, cancellationToken);
        if (user == null)
        {
          ModelState.AddModelError("", "Neplatné meno, alebo heslo.");
        }
        else
        {
          var claims = new List<Claim>
          {
            new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.GivenName, user.FullName)
          };

          var claimsIdentity = new ClaimsIdentity(claims, "password");
          var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);
          var properties = new AuthenticationProperties
          {
            ExpiresUtc = DateTime.UtcNow.AddHours(12)
          };

          await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrinciple, properties);

          if (Url.IsLocalUrl(model.ReturnUrl))
          {
            return Redirect(model.ReturnUrl);
          }
          return Redirect("~/");
        }
      }

      return View(model);
    }

    #endregion

    #region Logout
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

      return RedirectToAction(nameof(Login), new { ReturnUrl = "~/" });
    }
    #endregion
  }
}
