using System;
using System.Linq;
using System.Web.Mvc;
using UserManager.Models;
using UserManager.ViewModels.Users;

namespace UserManager.Controllers
{
	public class UsersController : BaseController
	{
		private UserManagerEntities context = new UserManagerEntities();

		public ActionResult Index()
		{
			var users = context.Users.AsEnumerable();

			return View(users);
		}

		public ActionResult Details(int id)
		{
			var user = context.Users.FirstOrDefault(u => u.UserId == id);
			if (user == null)
				return HttpNotFound();

			return View(user);
		}

		public ActionResult Create()
		{
			var model = new CreateViewModel();

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// Verify username isn't already taken
					var user = context.Users.FirstOrDefault(u => u.Username == model.Username);
					if (user != null)
						throw new Exception("Username", new Exception("Username already taken"));

					// Create new user and add to context
					context.Users.Add(new User()
					{
						Username = model.Username
					});

					// Save changes to the database
					context.SaveChanges();

					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					HandleException(ex);
				}
			}

			return View(model);
		}

		public ActionResult Edit(int id)
		{
			var user = context.Users.FirstOrDefault(u => u.UserId == id);
			if (user == null)
				return HttpNotFound();

			var model = new EditViewModel()
			{
				UserId = user.UserId,
				Username = user.Username
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// Verify user we want to edit exists
					var user = (from u in context.Users where u.UserId == model.UserId select u).FirstOrDefault();
					if (user == null)
						return HttpNotFound();

					// Verify username isn't already taken by another user
					if (context.Users.Any(u => u.Username == model.Username && u.UserId != model.UserId))
						throw new Exception("Username", new Exception("Username already taken"));

					// Update username value
					user.Username = model.Username;

					// Save changes to the database
					context.SaveChanges();

					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					HandleException(ex);
				}
			}

			return View(model);
		}

		public ActionResult Delete(int id)
		{
			try
			{
				//// Verify user we want to delete exists
				var user = context.Users.FirstOrDefault(u => u.UserId == id);
				if (user == null)
					return HttpNotFound();

				//// Remove user from context
				context.Users.Remove(user);

				//// Save changes to the database
				context.SaveChanges();

				return RedirectToAction("Index");
			}
			catch
			{
				return RedirectToAction("Error", "Home");
			}
		}
	}
}