using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Expenses.Web.Business.Extensions
{
  public static class ControllerExtensions
  {
    // https://docs.microsoft.com/en-us/aspnet/mvc/overview/security/preventing-open-redirection-attacks
    public static bool IsLocalUrl(string url)
    {
      if (string.IsNullOrEmpty(url))
      {
        return false;
      }
      else
      {
        return ((url[0] == '/' && (url.Length == 1 ||
                (url[1] != '/' && url[1] != '\\'))) ||   // "/" or "/foo" but not "//" or "/\"
                (url.Length > 1 &&
                 url[0] == '~' && url[1] == '/'));   // "~/" or "~/foo"
      }
    }

    public static string GetReturnUrl(this Controller controller)
    {
      if (controller == null)
      {
        throw new ArgumentNullException(nameof(controller));
      }

      const string str = "ReturnUrl";

      var returnUrl = controller.Request.Query.FirstOrDefault(it =>
      {
        return it.Key.Equals(str, StringComparison.InvariantCultureIgnoreCase);
      });

      if (returnUrl.IsNullOrDefault())
      {
        returnUrl = controller.Request.Form.FirstOrDefault(it =>
        {
          return it.Key.Equals(str, StringComparison.InvariantCultureIgnoreCase);
        });
      }

      if (returnUrl.IsNullOrDefault())
      {
        return null;
      }

      return returnUrl.Value.SingleOrDefault();
    }

    public static Guid GetUserId(this Controller controller)
    {
      if (controller == null)
      {
        throw new ArgumentNullException(nameof(controller));
      }

      if (!controller.User.Identity.IsAuthenticated)
      {
        throw new InvalidOperationException("User not authenticated.");
      }

      if (!(controller.User.Identity is ClaimsIdentity))
      {
        throw new InvalidOperationException("Invalid user identity.");
      }

      var identity = controller.User.Identity as ClaimsIdentity;

      var claim = identity.Claims.FirstOrDefault(it => it.Type == ClaimTypes.NameIdentifier);
      if (claim == null)
      {
        throw new InvalidOperationException("Unable to identify user from identity, invalid claims.");
      }

      if (!Guid.TryParse(claim.Value, out Guid id))
      {
        throw new InvalidOperationException("Unable to parse claim value for identification.");
      }

      return id;
    }
  }
}
