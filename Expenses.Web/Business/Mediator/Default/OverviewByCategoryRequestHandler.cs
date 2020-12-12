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
  public class OverviewByCategoryRequestHandler : IRequestHandler<OverviewByCategoryRequest, Dictionary<string, Overview>>
  {
    private readonly ExpensesDbContext context;

    public OverviewByCategoryRequestHandler(ExpensesDbContext context)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Dictionary<string, Overview>> Handle(OverviewByCategoryRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      // note: divided into two parts, because pesky expression
      // translator didn't like it.
      var list = await context.TransactionSet
        .Include(it => it.Category)
        .Where(it => it.UserId == request.UserId)
        .ToListAsync(cancellationToken);

      var dictionary = list
        .GroupBy(it => it.Category.Name)
        .Select(it => new
        {
          Category = it.Key,
          Credits = it.Where(it => it.Type == TransactionType.Credit),
          Debets = it.Where(it => it.Type == TransactionType.Debet)
        })
        .Select(it => new
        {
          it.Category,

          Credit = it.Credits.Sum(it => it.Amount),
          Debet = it.Debets.Sum(it => it.Amount),

          CreditWithoutPending = it.Credits.Where(it => !it.Pending).Sum(it => it.Amount),
          DebetWithoutPending = it.Debets.Where(it => !it.Pending).Sum(it => it.Amount),

          ContainsPendingCredit = it.Credits.Any(it => it.Pending),
          ContainsPendingDebet = it.Debets.Any(it => it.Pending)
        })
        .ToDictionary(
          k => k.Category,
          v => new Overview(v.Credit, v.Debet)
          {
            CreditWithoutPending = v.CreditWithoutPending,
            ContainsPendingCredit = v.ContainsPendingCredit,
            DebetWithoutPending = v.DebetWithoutPending,
            ContainsPendingDebet = v.ContainsPendingDebet
          }
        );

      return dictionary;
    }
  }
}
