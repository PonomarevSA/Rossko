using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rossko_test.Services;
using Microsoft.EntityFrameworkCore;
using Rossko_test.Model;
using Microsoft.Extensions.Configuration;
using Rossko_test.Core;
using Rossko_test.Interfaces;

namespace Rossko_test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RosskoContext>(options =>
                options.UseSqlServer(connection));
            services.AddMvc();

            services.AddTransient<IValidate, MyValidate>();
            services.AddTransient<IJsonResult, MyJsonResult>();
            services.AddTransient<IOptionResult, MyOptionResult>();
            services.AddTransient<IOptionsArray, MyOptionsArray>();

            services.AddScoped<IRosskoRepository, RosskoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptionsArray optionsArray)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
