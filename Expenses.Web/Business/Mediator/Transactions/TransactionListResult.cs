using Expenses.Web.ViewModels;
using System.Collections.Generic;

namespace Expenses.Web.Business.Mediator.Transactions
{
  public class TransactionListResult
  {
    public int Count { get; set; }

    public int Pages { get; set; }

    public int Index { get; set; }

    public List<TransactionViewModel> Data { get; set; }
  }
}
