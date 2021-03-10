using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.StandingOrders
{
  public class StandingOrdersListRequest : IRequest<StandingOrdersListResult>
  {
    public Guid UserId { get; set; }

    public StandingOrdersListRequest(Guid userId)
    {
      UserId = userId;
    }
  }
}
