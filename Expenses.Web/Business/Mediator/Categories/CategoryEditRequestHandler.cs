using Expenses.Web.Business.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expenses.Web.Business.Mediator.Categories
{
  public class CategoryEditRequestHandler : IRequestHandler<CategoryEditRequest, bool>
  {
    private readonly ExpensesDbContext context;

    public CategoryEditRequestHandler(ExpensesDbContext context)
    {
      this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> Handle(CategoryEditRequest request, CancellationToken cancellationToken)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var category = await context.CategorySet.FirstOrDefaultAsync(it => it.Id == request.Id, cancellationToken);
      // check if we aren't editing someone else's category
      if (category != null && category.UserId != request.UserId)
      {
        return false;
      }

      // new category, otherwise we're editing our existing one
      if (category == null)
      {
        category = new Category
        {
          UserId = request.UserId
        };

        await context.CategorySet.AddAsync(category, cancellationToken);
      }

      category.Name = request.Name;
      category.Description = request.Description;

      await context.SaveChangesAsync(cancellationToken);

      return true;
    }
  }
}
