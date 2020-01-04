using System.Linq;

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Demo.SinjulMSBH.Conventions
{
	public class HomePageRouteModelConvention : IPageRouteModelConvention
	{
		public void Apply(PageRouteModel model)
		{
			if (model.RelativePath == "/MyPages/Index.cshtml")
			{
				SelectorModel currentHomePage =
					model.Selectors.Single(s => string.IsNullOrEmpty(s.AttributeRouteModel.Template));

				model.Selectors.Remove(currentHomePage);
			}

			if (model.RelativePath.Contains("MyPages/Test"))
			{
				model.Selectors.Add(new SelectorModel()
				{
					AttributeRouteModel = new AttributeRouteModel
					{
						Template = string.Empty
					}
				});
			}
		}
	}
}
