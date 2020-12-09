using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.Transactions
{
  public class TransactionListRequest : IRequest<TransactionListResult>
  {
    public Guid UserId { get; set; }

    public int PageSize { get; set; } = 25;

    public int Page { get; set; } = 0;

    public TransactionListRequest(Guid userId)
    {
      UserId = userId;
    }
  }
}
