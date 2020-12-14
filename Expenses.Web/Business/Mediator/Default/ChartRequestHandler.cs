using Expenses.Web.Business.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.Default
{
  public class ChartRequestHandler : IRequestHandler<ChartRequest, ChartResponse>
  {
    private readonly ExpensesDbContext context;

    public ChartRequestHandler(ExpensesDbContext context)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ChartResponse> Handle(ChartRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var list = await context.TransactionSet
        .Include(it => it.Category)
        .Where(it => it.UserId == request.UserId)
        .Where(it => it.Created >= request.Start && it.Created <= request.End)
        .OrderBy(it => it.Created)
        .ToListAsync(cancellationToken);

      // TODO: this is not correct, rewrite

      var items = list
        .GroupBy(it => it.Created.Date)
        .Select(it => new ChartResponseItem
        {
          Date = it.Key,
          Value = it.Select(t => t.Type == TransactionType.Credit ? t.Amount : -t.Amount).Sum()
        })
        .ToArray();

      return new ChartResponse
      {
        Items = items
      };
    }
  }
}
