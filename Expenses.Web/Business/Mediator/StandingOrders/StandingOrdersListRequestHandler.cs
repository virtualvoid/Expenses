using AutoMapper;
using Expenses.Web.Business.Data;
using Expenses.Web.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.StandingOrders
{
  public class StandingOrdersListRequestHandler : IRequestHandler<StandingOrdersListRequest, StandingOrdersListResult>
  {
    private readonly ExpensesDbContext context;
    private readonly IMapper mapper;

    public StandingOrdersListRequestHandler(ExpensesDbContext context, IMapper mapper)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<StandingOrdersListResult> Handle(StandingOrdersListRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var categories = await context.StandingOrderSet
        .Include(it => it.Category)
        .Include(it => it.Account)
        .Where(it => it.UserId == request.UserId)
        .OrderBy(it => it.Installment)
        .ToListAsync(cancellationToken);

      var result = mapper.Map<List<StandingOrderViewModel>>(categories);

      return new StandingOrdersListResult
      {
        Data = result
      };
    }
  }
}
