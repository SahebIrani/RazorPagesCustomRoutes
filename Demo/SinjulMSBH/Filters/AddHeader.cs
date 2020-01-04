
using Microsoft.AspNetCore.Mvc.Filters;
namespace Demo.SinjulMSBH.Filters
{
	public class AddHeaderAttribute : ResultFilterAttribute
	{
		public AddHeaderAttribute(string name, string[] values)
		{
			_name = name;
			_values = values;
		}

		private readonly string _name;
		private readonly string[] _values;


		public override void OnResultExecuting(ResultExecutingContext context)
		{
			context.HttpContext.Response.Headers.Add(_name, _values);

			base.OnResultExecuting(context);
		}
	}
}
