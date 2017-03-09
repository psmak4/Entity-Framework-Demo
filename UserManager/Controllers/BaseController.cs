using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserManager.Controllers
{
    public class BaseController : Controller
    {
		protected void HandleException(Exception ex)
		{
			if (ex.InnerException != null)
				ModelState.AddModelError(ex.Message, ex.InnerException.Message);
			else
				ModelState.AddModelError("", ex.Message);
		}
	}
}