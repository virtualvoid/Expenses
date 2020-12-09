using MediatR;

namespace Expenses.Web.Business.Mediator.User
{
  public class RegistrationRequest : IRequest<RegistrationResult>
  {
    public string UserName { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
  }
}
