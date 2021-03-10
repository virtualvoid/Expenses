using Expenses.Web.Business.Data;
using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.StandingOrders
{
  public class StandingOrderEditRequest : IRequest<bool>
  {
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid UserId { get; set; }

    public TransactionType Type { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public DateTime Installment { get; set; }
  }
}
