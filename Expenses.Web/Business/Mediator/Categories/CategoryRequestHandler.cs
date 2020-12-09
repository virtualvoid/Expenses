using AutoMapper;
using Expenses.Web.Business.Data;
using Expenses.Web.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.Categories
{
  public class CategoryRequestHandler : IRequestHandler<CategoryRequest, CategoryViewModel>
  {
    private readonly ExpensesDbContext context;
    private readonly IMapper mapper;

    public CategoryRequestHandler(ExpensesDbContext context, IMapper mapper)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CategoryViewModel> Handle(CategoryRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var category = await context.CategorySet.FirstOrDefaultAsync(it => it.Id == request.Id, cancellationToken);
      if (category != null && category.UserId != request.UserId)
      {
        return null;
      }

      return mapper.Map<CategoryViewModel>(category);
    }
  }
}
