using Expenses.Web.Business.Data;
using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.Transactions
{
  public class TransactionEditRequest : IRequest<bool>
  {
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid UserId { get; set; }

    public TransactionType Type { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public bool Pending { get; set; }
  }
}
