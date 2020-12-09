using System;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Web.ViewModels
{
  public class TransactionViewModel
  {
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }


    public AccountViewModel Account { get; set; }

    public Guid CategoryId { get; set; }

    public CategoryViewModel Category { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }

    [Required]
    public string Type { get; set; }

    [MaxLength(256)]
    public string Description { get; set; }

    [Range(0.0, double.MaxValue)] // this is weird
    //[DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    public decimal Amount { get; set; }

    public bool Pending { get; set; }
  }
}
