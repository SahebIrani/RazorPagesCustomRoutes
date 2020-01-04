using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.SinjulMSBH.Filters
{
	public class EmptyFilter : IAsyncActionFilter
	{
		public EmptyFilter()
		{
		}

		public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			return Task.CompletedTask;
		}

		public async Task OnActionExecutedAsync(ActionExecutedContext context)
		{
			await Task.CompletedTask;
		}
	}
}
