using Expenses.Web.Business.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.Account
{
  public class AccountEditRequestHandler : IRequestHandler<AccountEditRequest, bool>
  {
    private readonly ExpensesDbContext context;

    public AccountEditRequestHandler(ExpensesDbContext context)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> Handle(AccountEditRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var account = await context.AccountSet.FirstOrDefaultAsync(it => it.Id == request.Id, cancellationToken);
      // check if we aren't editing someone else's account
      if (account != null && account.UserId != request.UserId)
      {
        return false;
      }

      // new account, otherwise we're editing our existing one
      if (account == null)
      {
        account = new Data.Account
        {
          UserId = request.UserId,
          Created = DateTime.UtcNow
        };

        await context.AccountSet.AddAsync(account, cancellationToken);
      }

      account.Name = request.Name;
      account.IBAN = request.IBAN;

      await context.SaveChangesAsync(cancellationToken);

      return true;
    }
  }
}
