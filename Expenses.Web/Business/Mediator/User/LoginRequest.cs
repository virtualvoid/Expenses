using MediatR;

namespace Expenses.Web.Business.Mediator.User
{
  public class LoginRequest : IRequest<LoginResult>
  {
    public string UserName { get; set; }

    public string Password { get; set; }
  }
}
