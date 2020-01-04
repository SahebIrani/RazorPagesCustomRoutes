using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.MyPages.Admin
{
	public class MyTestingModel : PageModel
	{
		public string Message { get; private set; }

		public string RouteDataGlobalTemplateValue { get; private set; }

		public string RouteDataMyTestingTemplateeValue { get; private set; }

		public string RouteDataTextTemplateValue { get; private set; }

		public void Get()
		{
			Message = "Your application description page.";

			if (RouteData.Values["globalTemplate"] != null)
				RouteDataGlobalTemplateValue = $"Route data for 'globalTemplate' was provided: {RouteData.Values["globalTemplate"]}";

			if (RouteData.Values["myTestingTemplate"] != null)
				RouteDataMyTestingTemplateeValue = $"Route data for 'myTestingTemplate' was provided: {RouteData.Values["myTestingTemplate"]}";

			if (RouteData.Values["text"] != null)
				RouteDataTextTemplateValue = $"Route data for 'text' was provided: {RouteData.Values["text"]}";
		}
	}
}
