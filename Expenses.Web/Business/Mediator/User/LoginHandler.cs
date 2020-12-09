using Expenses.Web.Business.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.User
{
  public class LoginHandler : IRequestHandler<LoginRequest, LoginResult>
  {
    private readonly ExpensesDbContext context;

    public LoginHandler(ExpensesDbContext context)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<LoginResult> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var passwordHash = await MD5.HashAsync(request.Password, cancellationToken);

      var user = await context.UserSet.FirstOrDefaultAsync(
        it => it.UserName.ToLower() == request.UserName.ToLower() &&
              it.Password == passwordHash,
        cancellationToken
      );

      if (user == null)
      {
        return null;
      }

      return new LoginResult
      {
        Id = user.Id,
        UserName = user.UserName,
        FullName = $"{user.LastName}, {user.FirstName}"
      };
    }
  }
}
