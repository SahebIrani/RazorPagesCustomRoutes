using Demo.SinjulMSBH.Conventions;
using Demo.SinjulMSBH.Data;
using Demo.SinjulMSBH.Factories;
using Demo.SinjulMSBH.Filters;
using Demo.SinjulMSBH.Transformers;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Environment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContextPool<ApplicationDbContext>(options =>
				options.UseInMemoryDatabase(nameof(ApplicationDbContext)));


			IMvcBuilder builder = services.AddRazorPages();

			builder
				.AddRazorPagesOptions(options =>
				{
					options.RootDirectory = "/MyPages";

					options.Conventions
						.AuthorizeFolder("/Admin")
						.AllowAnonymousToPage("/Admin/Anonymous")
						.AllowAnonymousToPage("/Admin/MyTesting")
					;

					options.Conventions.AddPageRoute("/Test", "/");

					options.Conventions.AddPageRoute("/Admin/MyTesting", "TheMyTestingPage/{text?}");

					options.Conventions.Add(new GlobalTemplatePageRouteModelConvention());
					options.Conventions.Add(new GlobalHeaderPageApplicationModelConvention());
					options.Conventions.Add(new GlobalPageHandlerModelConvention());


					options.Conventions.Add(new HomePageRouteModelConvention());

					options.Conventions.ConfigureFilter(new AddHeaderWithFactory());

					options.Conventions.Add(new PageRouteTransformerConvention(new SlugifyParameterTransformer()));

					options.Conventions.AddFolderRouteModelConvention("/Admin", model =>
					{

					});

					options.Conventions.AddPageRouteModelConvention("/Admin/MyTesting", model =>
					{
						int selectorCount = model.Selectors.Count;

						for (int i = 0; i < selectorCount; i++)
						{
							SelectorModel selector = model.Selectors[i];
							model.Selectors.Add(new SelectorModel
							{
								AttributeRouteModel = new AttributeRouteModel
								{
									Order = 2,
									Template = AttributeRouteModel.CombineTemplates(
										selector.AttributeRouteModel.Template,
										"{myTestingTemplate?}"),
								}
							});
						}
					});


					options.Conventions.AddFolderApplicationModelConvention("/Admin", model =>
					{
						model.Filters.Add(new AddHeaderAttribute(
							"AdminPagesHeader", new string[] { "AdminPages Header Value" })
						);
					});

					options.Conventions.AddPageApplicationModelConvention("/Test", model =>
					{
						model.Filters.Add(new AddHeaderAttribute(
							"TestHeader", new string[] { "Test Header Value" })
						);
					});

					options.Conventions.ConfigureFilter(model =>
					{
						if (model.RelativePath.Contains("Admin/MyTesting"))
							return new AddHeaderAttribute(
								"MyTestingOtherHeader", new string[] { "MyTestingOther Header Value" }
							);

						return new EmptyFilter();
					});

				})
				//.WithRazorPagesAtContentRoot()
				//.WithRazorPagesRoot("")
				.SetCompatibilityVersion(CompatibilityVersion.Latest)
			;

#if DEBUG
			if (Environment.IsDevelopment())
				builder.AddRazorRuntimeCompilation();
#endif




		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app)
		{
			if (Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
