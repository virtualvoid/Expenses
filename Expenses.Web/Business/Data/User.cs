using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Web.Business.Data
{
  [Table("users")]
  public class User
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(32)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(96)]
    public string Password { get; set; }

    [MaxLength(48)]
    public string FirstName { get; set; }

    [MaxLength(48)]
    public string LastName { get; set; }

    public virtual ICollection<Category> Categories { get; set; }

    public virtual ICollection<Account> Accounts { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; }
  }
}
