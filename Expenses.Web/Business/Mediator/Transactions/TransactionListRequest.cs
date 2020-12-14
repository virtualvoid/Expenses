using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.Transactions
{
  public class TransactionListRequest : IRequest<TransactionListResult>
  {
    public Guid UserId { get; set; }

    public int PageSize { get; set; } = 10;

    public int Page { get; set; } = 0;

    public Guid? CategoryId { get; set; }

    public bool? Pending { get; set; }

    public TransactionListRequest(Guid userId)
    {
      UserId = userId;
    }
  }
}
