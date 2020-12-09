using Expenses.Web.ViewModels;
using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.Account
{
  public class AccountRequest : IRequest<AccountViewModel>
  {
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public AccountRequest(Guid id, Guid userId)
    {
      Id = id;
      UserId = userId;
    }
  }
}
