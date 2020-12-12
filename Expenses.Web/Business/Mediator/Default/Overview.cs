namespace Expenses.Web.Business.Mediator.Default
{
  public class Overview
  {
    public decimal Credit { get; set; }

    public decimal CreditWithoutPending { get; set; }

    public bool ContainsPendingCredit { get; set; }

    public decimal Debet { get; set; }

    public decimal DebetWithoutPending { get; set; }

    public bool ContainsPendingDebet { get; set; }

    public Overview(
      decimal credit,
      decimal debet
    )
    {
      Credit = credit;
      Debet = debet;
    }
  }
}
