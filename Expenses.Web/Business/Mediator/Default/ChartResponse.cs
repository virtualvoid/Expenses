using System;

namespace Expenses.Web.Business.Mediator.Default
{
  public class ChartResponse
  {
    public ChartResponseItem[] Items { get; set; }
  }

  public class ChartResponseItem
  {
    public DateTime Date { get; set; }

    public decimal Value { get; set; }
  }
}
