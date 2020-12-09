using Expenses.Web.Business.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.User
{
  public class RegistrationHandler : IRequestHandler<RegistrationRequest, RegistrationResult>
  {
    private readonly ExpensesDbContext context;

    public RegistrationHandler(ExpensesDbContext context)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<RegistrationResult> Handle(RegistrationRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var alreadyExisting = await context.UserSet.AnyAsync(
        it => it.UserName.ToLower() == request.UserName.ToLower(),
        cancellationToken
      );

      if (alreadyExisting)
      {
        return new RegistrationResult
        {
          Success = false,
          Message = $"Username `{request.UserName}` is already taken."
        };
      }

      var passwordHash = await MD5.HashAsync(request.Password, cancellationToken);

      var user = new Data.User
      {
        UserName = request.UserName,
        Password = passwordHash,

        FirstName = request.FirstName,
        LastName = request.LastName
      };

      await context.UserSet.AddAsync(user, cancellationToken);
      await context.SaveChangesAsync(cancellationToken);

      return new RegistrationResult
      {
        Success = true
      };
    }
  }
}
