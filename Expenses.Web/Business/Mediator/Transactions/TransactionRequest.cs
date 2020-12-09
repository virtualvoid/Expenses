using Expenses.Web.ViewModels;
using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.Transactions
{
  public class TransactionRequest : IRequest<TransactionViewModel>
  {
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public TransactionRequest(Guid id, Guid userId)
    {
      Id = id;
      UserId = userId;
    }
  }
}
