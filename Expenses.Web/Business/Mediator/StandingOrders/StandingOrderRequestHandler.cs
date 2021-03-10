using AutoMapper;
using Expenses.Web.Business.Data;
using Expenses.Web.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.StandingOrders
{
  public class StandingOrderRequestHandler : IRequestHandler<StandingOrderRequest, StandingOrderViewModel>
  {
    private readonly ExpensesDbContext context;
    private readonly IMapper mapper;

    public StandingOrderRequestHandler(ExpensesDbContext context, IMapper mapper)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<StandingOrderViewModel> Handle(StandingOrderRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var standingOrder = await context.StandingOrderSet.FirstOrDefaultAsync(it => it.Id == request.Id, cancellationToken);
      if (standingOrder != null && standingOrder.UserId != request.UserId)
      {
        return null;
      }

      return mapper.Map<StandingOrderViewModel>(standingOrder);
    }
  }
}
