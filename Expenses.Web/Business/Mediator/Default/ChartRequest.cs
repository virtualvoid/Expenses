using MediatR;
using System;

namespace Expenses.Web.Business.Mediator.Default
{
  public class ChartRequest : IRequest<ChartResponse>
  {
    public Guid UserId { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public ChartRequest(Guid userId)
    {
      UserId = userId;
    }

    public static ChartRequest Last30Days(Guid userId)
    {
      var utcNow = DateTime.UtcNow;

      var start = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day).AddDays(-30);
      var end = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day).AddDays(1).AddSeconds(-1);

      return new ChartRequest(userId)
      {
        Start = start,
        End = end
      };
    }

    public static ChartRequest CurrentMonth(Guid userId)
    {
      var utcNow = DateTime.UtcNow;

      var start = new DateTime(utcNow.Year, utcNow.Month, 1);
      var end = start.AddMonths(1).AddSeconds(-1);

      return new ChartRequest(userId)
      {
        Start = start,
        End = end
      };
    }
  }
}
