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

namespace Expenses.Web.Business.Mediator.Transactions
{
  public class TransactionListRequestHandler : IRequestHandler<TransactionListRequest, TransactionListResult>
  {
    private readonly ExpensesDbContext context;
    private readonly IMapper mapper;

    public TransactionListRequestHandler(ExpensesDbContext context, IMapper mapper)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<TransactionListResult> Handle(TransactionListRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var totalCount = await context.TransactionSet
        .Where(it => it.UserId == request.UserId)
        .CountAsync(cancellationToken);

      var pageCount = totalCount > 0 ? (totalCount / request.PageSize) - (totalCount % request.PageSize == 0 ? 1 : 0) : 1;

      var accounts = await context.TransactionSet
        .Include(it => it.Category)
        .Include(it => it.Account)
        .Where(it => it.UserId == request.UserId)
        .OrderByDescending(it => it.Created)
        .Skip(request.PageSize * request.Page)
        .Take(request.PageSize)
        .ToListAsync(cancellationToken);

      var data = mapper.Map<List<TransactionViewModel>>(accounts);

      return new TransactionListResult
      {
        Count = totalCount,
        Pages = pageCount,
        Index = request.Page,
        Data = data
      };
    }
  }
}
