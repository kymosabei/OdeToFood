using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OdeToFood.Services;
using Microsoft.AspNet.Routing;
using OdeToFood.Entities;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.PlatformAbstractions;

namespace OdeToFood
{
    // Testing
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(Configuration["database:connection"]));

            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<OdeToFoodDbContext>();

            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IGreeter, Greeter>();
            //services.AddScoped<IRestaurantData, InMemoryRestaurantData>();
            services.AddScoped<IRestaurantData, SqlRestuarantData>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment, IGreeter greeter, IApplicationEnvironment appEnvironment)
        {
            app.UseIISPlatformHandler();

            //app.UseWelcomePage();

            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRuntimeInfoPage("/info");

            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            app.UseFileServer();

            app.UseNodeModules(appEnvironment);

            //app.UseMvc(ConfigureRoute);

            app.UseIdentity();

            app.UseMvc(ConfigureRoutes);

            //app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                //var greeting = Configuration["greeting"];
                //await context.Response.WriteAsync("Hello World!!!");

                var greeting = greeter.GetGreeting();
                await context.Response.WriteAsync(greeting);
            });
        }

        //private void ConfigureRoute(IRouteBuilder routeBuilder)
        //{
        //    // /home/index

        //    // Convention Based
        //    routeBuilder.MapRoute("Default", 
        //        "{controller=Home}/{action=Index}/{id?}");
        //}

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // Attribute Based
            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
