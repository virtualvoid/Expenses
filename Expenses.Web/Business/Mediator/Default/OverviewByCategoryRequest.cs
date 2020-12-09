using MediatR;
using System;
using System.Collections.Generic;

namespace Expenses.Web.Business.Mediator.Default
{
  public class OverviewByCategoryRequest : IRequest<Dictionary<string, Overview>>
  {
    public Guid UserId { get; set; }

    public OverviewByCategoryRequest(Guid userId)
    {
      UserId = userId;
    }
  }
}
