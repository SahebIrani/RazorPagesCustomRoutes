using System;

using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.SinjulMSBH.Factories
{
	public class AddHeaderWithFactory : IFilterFactory
	{
		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) => new AddHeaderFilter();

		private class AddHeaderFilter : IResultFilter
		{
			public void OnResultExecuting(ResultExecutingContext context) =>
				context.HttpContext.Response.Headers.Add(
					"FilterFactoryHeader",
					new string[]
					{
						"Filter Factory Header Value 1",
						"Filter Factory Header Value 2"
					})
				;

			public void OnResultExecuted(ResultExecutedContext context)
			{
			}
		}

		public bool IsReusable => false;
	}
}
