using Expenses.Web.Business.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.StandingOrders
{
  public class StandingOrderProceedRequestHandler : IRequestHandler<StandingOrderProceedRequest, bool>
  {
    private readonly ExpensesDbContext context;

    public StandingOrderProceedRequestHandler(ExpensesDbContext context)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> Handle(StandingOrderProceedRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var standingOrder = await context.StandingOrderSet.FirstOrDefaultAsync(it => it.Id == request.Id, cancellationToken);
      if (standingOrder != null && standingOrder.UserId != request.UserId)
      {
        return false;
      }

      var newTransaction = new Transaction
      {
        UserId = standingOrder.UserId,
        AccountId = standingOrder.AccountId,
        CategoryId = standingOrder.CategoryId,

        Created = DateTime.UtcNow,

        Type = standingOrder.Type,
        Description = standingOrder.Description,
        Amount = standingOrder.Amount
      };

      await context.TransactionSet.AddAsync(newTransaction, cancellationToken);

      standingOrder.Installment = DateTime.UtcNow.AddMonths(1);

      await context.SaveChangesAsync(cancellationToken);

      return true;
    }
  }
}
