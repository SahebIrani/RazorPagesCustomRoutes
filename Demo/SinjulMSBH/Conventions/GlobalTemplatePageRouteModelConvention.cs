using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Demo.SinjulMSBH.Conventions
{
	public class GlobalTemplatePageRouteModelConvention : IPageRouteModelConvention
	{
		public void Apply(PageRouteModel model)
		{
			int selectorCount = model.Selectors.Count;

			for (int i = 0; i < selectorCount; i++)
			{
				SelectorModel selector = model.Selectors[i];

				model.Selectors.Add(new SelectorModel
				{
					AttributeRouteModel = new AttributeRouteModel
					{
						Order = 1,

						Template = AttributeRouteModel.CombineTemplates(
							selector.AttributeRouteModel.Template,
							"{globalTemplate?}"),
					}
				});
			}
		}
	}
}
