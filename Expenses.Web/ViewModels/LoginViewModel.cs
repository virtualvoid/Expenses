using System.ComponentModel.DataAnnotations;

namespace Expenses.Web.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
  }
}
