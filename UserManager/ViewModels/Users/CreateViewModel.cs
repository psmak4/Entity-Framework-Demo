using System.ComponentModel.DataAnnotations;

namespace UserManager.ViewModels.Users
{
	public class CreateViewModel
	{
		[Required]
		public string Username { get; set; }
	}
}