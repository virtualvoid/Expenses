using AutoMapper;
using Expenses.Web.Business.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.Transactions
{
  public class TransactionEditRequestHandler : IRequestHandler<TransactionEditRequest, bool>
  {
    private readonly ExpensesDbContext context;
    private readonly IMapper mapper;

    public TransactionEditRequestHandler(ExpensesDbContext context, IMapper mapper)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<bool> Handle(TransactionEditRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var transaction = await context.TransactionSet.FirstOrDefaultAsync(it => it.Id == request.Id, cancellationToken);
      // check if we aren't editing someone else's transaction
      if (transaction != null && transaction.UserId != request.UserId)
      {
        return false;
      }

      if (transaction == null)
      {
        transaction = new Transaction
        {
          UserId = request.UserId,
          Created = DateTime.UtcNow,
        };

        await context.TransactionSet.AddAsync(transaction, cancellationToken);
      }
      else
      {
        transaction.Modified = DateTime.UtcNow;
      }

      transaction.AccountId = request.AccountId;
      transaction.CategoryId = request.CategoryId;
      transaction.Type = request.Type;
      transaction.Description = request.Description;
      transaction.Amount = request.Amount;
      transaction.Pending = request.Pending;

      await context.SaveChangesAsync(cancellationToken);

      return true;
    }
  }
}
