using Expenses.Web.ViewModels;
using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.StandingOrders
{
  public class StandingOrderRequest : IRequest<StandingOrderViewModel>
  {
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public StandingOrderRequest(Guid id, Guid userId)
    {
      Id = id;
      UserId = userId;
    }
  }
}
