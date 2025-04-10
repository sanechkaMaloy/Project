using System.ComponentModel.DataAnnotations;

namespace BlazorApp1
{
	public class LoginViewModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage =	"Please, provide username")]

		public string? Username { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Please, provide password")]

		public string? Password { get; set; }
	}
}
