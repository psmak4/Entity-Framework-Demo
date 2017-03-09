using System;
using System.Linq;
using System.Web.Mvc;
using UserManager.Models;
using UserManager.ViewModels.Posts;

namespace UserManager.Controllers
{
	public class PostsController : BaseController
	{
		private UserManagerEntities context = new UserManagerEntities();

		public ActionResult Index()
		{
			var posts = context.Posts.AsEnumerable();

			return View(posts);
		}

		public ActionResult Details(int id)
		{
			var post = context.Posts.FirstOrDefault(p => p.PostId == id);
			if (post == null)
				return HttpNotFound();

			return View(post);
		}

		public ActionResult Create()
		{
			var model = new CreateViewModel();
			model.UserOptions = context.Users.AsEnumerable();

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
					// Create new post and add to context
					context.Posts.Add(new Post()
					{
						UserId = model.UserId,
						Title = model.Title
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

			model.UserOptions = context.Users.AsEnumerable();

			return View(model);
		}

		public ActionResult Edit(int id)
		{
			var post = context.Posts.FirstOrDefault(p => p.PostId == id);
			if (post == null)
				return HttpNotFound();

			var model = new EditViewModel()
			{
				PostId = post.PostId,
				Title = post.Title,
				UserId = post.UserId,
				UserOptions = context.Users.AsEnumerable()
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
					// Verify post we want to edit exists
					var post = (from p in context.Posts where p.PostId == model.PostId select p).FirstOrDefault();
					if (post == null)
						return HttpNotFound();

					// Update user id and title
					post.UserId = model.UserId;
					post.Title = model.Title;

					// Save changes to the database
					context.SaveChanges();

					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					HandleException(ex);
				}
			}

			model.UserOptions = context.Users.AsEnumerable();

			return View(model);
		}

		public ActionResult Delete(int id)
		{
			try
			{
				// Use SQL to remove user from database
				context.Database.ExecuteSqlCommand("DELETE FROM dbo.Posts WHERE PostId = {0}", id);

				return RedirectToAction("Index");
			}
			catch
			{
				return RedirectToAction("Error", "Home");
			}
		}
	}
}