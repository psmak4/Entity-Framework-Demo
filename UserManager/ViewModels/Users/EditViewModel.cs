using System.ComponentModel.DataAnnotations;

namespace UserManager.ViewModels.Users
{
	public class EditViewModel
	{
		[Required]
		public int UserId { get; set; }

		[Required]
		public string Username { get; set; }
	}
}