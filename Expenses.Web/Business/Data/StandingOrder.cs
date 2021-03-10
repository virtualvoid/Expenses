using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Web.Business.Data
{
  [Table("standingorders")]
  public class StandingOrder
  {
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    [ForeignKey(nameof(Account))]
    public Guid AccountId { get; set; }

    [ForeignKey(nameof(AccountId))]
    public virtual Account Account { get; set; }

    [ForeignKey(nameof(Category))]
    public Guid CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }

    public TransactionType Type { get; set; }

    public decimal Amount { get; set; }

    [MaxLength(256)]
    public string Description { get; set; }

    public DateTime Installment { get; set; }
  }
}
