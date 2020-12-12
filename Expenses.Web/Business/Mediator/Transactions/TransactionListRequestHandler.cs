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

      var query = context.TransactionSet
        .Include(it => it.Category)
        .Include(it => it.Account)
        .Where(it => it.UserId == request.UserId);

      if (request.CategoryId.HasValue)
      {
        query = query
          .Where(it => it.CategoryId == request.CategoryId.Value);
      }

      var count = await query.CountAsync(cancellationToken);
      var pageCount = ((count - 1) / request.PageSize) + 1;

      var accounts = await query
        .OrderByDescending(it => it.Created)
        .Skip(request.PageSize * request.Page)
        .Take(request.PageSize)
        .ToListAsync(cancellationToken);

      var data = mapper.Map<List<TransactionViewModel>>(accounts);

      return new TransactionListResult
      {
        Count = count,
        Pages = pageCount,
        Index = request.Page,
        Data = data
      };
    }
  }
}
