using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserManager.Models;

namespace UserManager.ViewModels.Posts
{
	public class EditViewModel
	{
		[Required]
		public int PostId { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public int UserId { get; set; }

		public IEnumerable<User> UserOptions { get; set; }
	}
}