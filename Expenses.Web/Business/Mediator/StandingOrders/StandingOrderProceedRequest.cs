using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.StandingOrders
{
  public class StandingOrderProceedRequest : IRequest<bool>
  {
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public StandingOrderProceedRequest(Guid id, Guid userId)
    {
      Id = id;
      UserId = userId;
    }
  }
}
