using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.Account
{
  public class AccountEditRequest : IRequest<bool>
  {
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string IBAN { get; set; }

    public AccountEditRequest()
    {
    }

    public AccountEditRequest(Guid id, Guid userId)
    {
      Id = id;
      UserId = userId;
    }
  }
}
