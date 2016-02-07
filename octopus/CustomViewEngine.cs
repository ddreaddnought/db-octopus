using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace octopus
{
	public class CustomViewEngine : WebFormViewEngine
	{
		public CustomViewEngine()
		{
			var viewLocations = new[] {
				"~/Views/Shared/Cards/{0}.cshtml"
				};

			this.PartialViewLocationFormats = viewLocations;
			this.ViewLocationFormats = viewLocations;
		}

	}
}
