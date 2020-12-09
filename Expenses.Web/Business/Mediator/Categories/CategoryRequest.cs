using Expenses.Web.ViewModels;
using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.Categories
{
  public class CategoryRequest : IRequest<CategoryViewModel>
  {
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public CategoryRequest(Guid id, Guid userId)
    {
      Id = id;
      UserId = userId;
    }
  }
}
