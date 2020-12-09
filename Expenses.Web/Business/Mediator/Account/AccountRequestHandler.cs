using AutoMapper;
using Expenses.Web.Business.Data;
using Expenses.Web.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.Account
{
  public class AccountRequestHandler : IRequestHandler<AccountRequest, AccountViewModel>
  {
    private readonly ExpensesDbContext context;
    private readonly IMapper mapper;

    public AccountRequestHandler(ExpensesDbContext context, IMapper mapper)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AccountViewModel> Handle(AccountRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var account = await context.AccountSet.FirstOrDefaultAsync(it => it.Id == request.Id, cancellationToken);
      if (account != null && account.UserId != request.UserId)
      {
        return null;
      }

      return mapper.Map<AccountViewModel>(account);
    }
  }
}
