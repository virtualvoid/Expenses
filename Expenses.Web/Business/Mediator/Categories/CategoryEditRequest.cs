using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.Categories
{
  public class CategoryEditRequest : IRequest<bool>
  {
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public CategoryEditRequest()
    {
    }

    public CategoryEditRequest(Guid id, Guid userId)
    {
      Id = id;
      UserId = userId;
    }
  }
}
