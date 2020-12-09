using Expenses.Web.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace Expenses.Web.Business.Mediator.Account
{
  public class AccountListRequest : IRequest<List<AccountViewModel>>
  {
    public Guid UserId { get; set; }

    public AccountListRequest(Guid userId)
    {
      UserId = userId;
    }
  }
}
