using System.ComponentModel.DataAnnotations;

namespace Expenses.Web.ViewModels
{
  public class RegistrationViewModel
  {
    [Required, MaxLength(32)]
    public string UserName { get; set; }

    [Required, MaxLength(96)]
    public string Password { get; set; }

    [Required, MaxLength(96), Compare(nameof(Password))]
    public string PasswordAgain { get; set; }

    [MaxLength(48)]
    public string FirstName { get; set; }

    [MaxLength(48)]
    public string LastName { get; set; }
  }
}
