using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses.Web.Business.Data
{
  [Table("categories")]
  public class Category
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

    [MaxLength(256)]
    public string Description { get; set; }
  }
}
