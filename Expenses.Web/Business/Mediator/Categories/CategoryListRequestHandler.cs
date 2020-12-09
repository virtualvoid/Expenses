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

namespace Expenses.Web.Business.Mediator.Categories
{
  public class CategoryListRequestHandler : IRequestHandler<CategoryListRequest, List<CategoryViewModel>>
  {
    private readonly ExpensesDbContext context;
    private readonly IMapper mapper;

    public CategoryListRequestHandler(ExpensesDbContext context, IMapper mapper)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<CategoryViewModel>> Handle(CategoryListRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var categories = await context.CategorySet
        .Where(it => it.UserId == request.UserId)
        .OrderBy(it => it.Name)
        .ToListAsync(cancellationToken);

      var result = mapper.Map<List<CategoryViewModel>>(categories);
      return result;
    }
  }
}
