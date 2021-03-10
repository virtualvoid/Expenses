using Expenses.Web.Business.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.StandingOrders
{
  public class StandingOrderEditRequestHandler : IRequestHandler<StandingOrderEditRequest, bool>
  {
    private readonly ExpensesDbContext context;

    public StandingOrderEditRequestHandler(ExpensesDbContext context)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> Handle(StandingOrderEditRequest request, CancellationToken cancellationToken)
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

      if (standingOrder == null)
      {
        standingOrder = new StandingOrder
        {
          UserId = request.UserId,
        };

        await context.StandingOrderSet.AddAsync(standingOrder, cancellationToken);
      }

      standingOrder.AccountId = request.AccountId;
      standingOrder.CategoryId = request.CategoryId;
      standingOrder.Type = request.Type;
      standingOrder.Description = request.Description;
      standingOrder.Amount = request.Amount;
      standingOrder.Installment = request.Installment;

      await context.SaveChangesAsync(cancellationToken);

      return true;
    }
  }
}
