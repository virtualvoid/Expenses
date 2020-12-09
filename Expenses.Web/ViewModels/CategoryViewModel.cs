using System;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Web.ViewModels
{
  public class CategoryViewModel
  {
    public Guid Id { get; set; }

    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [MaxLength(256)]
    public string Description { get; set; }
  }
}
