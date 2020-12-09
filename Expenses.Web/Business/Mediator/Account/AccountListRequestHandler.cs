using AutoMapper;
using Expenses.Web.Business.Data;
using Expenses.Web.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.Account
{
  public class AccountListRequestHandler : IRequestHandler<AccountListRequest, List<AccountViewModel>>
  {
    private readonly ExpensesDbContext context;
    private readonly IMapper mapper;

    public AccountListRequestHandler(ExpensesDbContext context, IMapper mapper)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<AccountViewModel>> Handle(AccountListRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var accounts = await context.AccountSet
        .Where(it => it.UserId == request.UserId)
        .OrderBy(it => it.Name)
        .ThenBy(it => it.Created)
        .ToListAsync(cancellationToken);

      var result = mapper.Map<List<AccountViewModel>>(accounts);
      return result;
    }
  }
}
