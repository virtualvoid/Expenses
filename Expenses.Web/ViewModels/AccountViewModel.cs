using System;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Web.ViewModels
{
  public class AccountViewModel
  {
    public Guid Id { get; set; }

    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [MaxLength(64)]
    public string IBAN { get; set; }

    public DateTime Created { get; set; }
  }
}
