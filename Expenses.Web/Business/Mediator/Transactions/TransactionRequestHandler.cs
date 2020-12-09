using AutoMapper;
using Expenses.Web.Business.Data;
using Expenses.Web.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.Transactions
{
  public class TransactionRequestHandler : IRequestHandler<TransactionRequest, TransactionViewModel>
  {
    private readonly ExpensesDbContext context;
    private readonly IMapper mapper;

    public TransactionRequestHandler(ExpensesDbContext context, IMapper mapper)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<TransactionViewModel> Handle(TransactionRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var transaction = await context.TransactionSet.FirstOrDefaultAsync(it => it.Id == request.Id, cancellationToken);
      if (transaction != null && transaction.UserId != request.UserId)
      {
        return null;
      }

      return mapper.Map<TransactionViewModel>(transaction);
    }
  }
}
