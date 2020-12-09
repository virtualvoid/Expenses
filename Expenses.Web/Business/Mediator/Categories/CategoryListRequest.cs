using Expenses.Web.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace Expenses.Web.Business.Mediator.Categories
{
  public class CategoryListRequest : IRequest<List<CategoryViewModel>>
  {
    public Guid UserId { get; set; }

    public CategoryListRequest(Guid userId)
    {
      UserId = userId;
    }
  }
}
