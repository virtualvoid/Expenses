using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Web.Business.Data
{
  [Table("accounts")]
  public class Account
  {
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [MaxLength(64)]
    public string IBAN { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; }
  }
}
