namespace Expenses.Web.Business.Mediator.Default
{
  public class Overview
  {
    public decimal Credit { get; set; }

    public decimal CreditWithoutPending { get; set; }

    public decimal Debet { get; set; }

    public decimal DebetWithoutPending { get; set; }

    public Overview(
      decimal credit,
      decimal creditWithoutPending,
      decimal debet,
      decimal debetWithoutPending
    )
    {
      Credit = credit;
      CreditWithoutPending = creditWithoutPending;
      Debet = debet;
      DebetWithoutPending = debetWithoutPending;
    }
  }
}
