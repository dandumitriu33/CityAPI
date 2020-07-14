using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CityInfo.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddMvcOptions( o =>
                {
                    o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    //o.OutputFormatters.Remove(Json thing);
                    //o.OutputFormatters.Clear();
                });
            // there is a way to adapt to old Json info - working with serializer settings - variable names begin with Caps
            // .AddJsonOptions( o =>
            //{
            //  if (o.SerializerSettings.ContractResolver != null)
            //  {
            //      var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //      castedResolver.NamingStrategy = null;
            //  }
            //});
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            //app.UseStatusCodePages();  // returns a short line of text with the status code

            app.UseMvc();
            
        }
    }
}
