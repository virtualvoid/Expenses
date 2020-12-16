using Expenses.Web.Business.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

      var items = new List<ChartResponseItem>();

      var list = await context.TransactionSet
        .Include(it => it.Category)
        .Where(it => it.UserId == request.UserId)
        .Where(it => it.Created >= request.Start && it.Created <= request.End)
        .OrderBy(it => it.Created)
        .ToListAsync(cancellationToken);

      // TODO: maybe cleanup this mess
      if (list.Any())
      {
        var group = list.GroupBy(it => it.Created.Date)
          .ToDictionary(
            k => k.Key,
            v => v.Select(t => t.Type == TransactionType.Credit ? t.Amount : -t.Amount).Sum()
          );

        var keys = group.Keys.ToArray();
        items.Add(new ChartResponseItem(keys[0], group[keys[0]]));
        for (var i = 1; i < keys.Length; i++)
        {
          var key = keys[i];
          items.Add(new ChartResponseItem(key, items[i - 1].Value + group[key]));
        }
      }
      return new ChartResponse
      {
        Items = items
      };
    }
  }
}
