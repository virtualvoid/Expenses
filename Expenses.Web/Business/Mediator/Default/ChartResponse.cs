using System;
using System.Collections.Generic;

namespace Expenses.Web.Business.Mediator.Default
{
  public class ChartResponse
  {
    public List<ChartResponseItem> Items { get; set; }
  }

  public class ChartResponseItem
  {
    public DateTime Date { get; set; }

    public decimal Value { get; set; }

    public ChartResponseItem(DateTime date, decimal value)
    {
      Date = date;
      Value = value;
    }
  }
}
