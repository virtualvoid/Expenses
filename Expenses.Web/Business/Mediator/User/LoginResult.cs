using System;

namespace Expenses.Web.Business.Mediator.User
{
  public class LoginResult
  {
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string FullName { get; set; }
  }
}
