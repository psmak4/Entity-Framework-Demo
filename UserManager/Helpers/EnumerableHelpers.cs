using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
	public static class EnumerableHelpers
	{
		public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> enumerable, Func<T, string> text, Func<T, string> value)
		{
			return enumerable.Select(x => new SelectListItem()
			{
				Text = text(x),
				Value = value(x)
			}).AsEnumerable();
		}
	}
}