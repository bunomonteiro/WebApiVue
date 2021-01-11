
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspCoreVuejs.Api
{
    public class Startup
    {
        public string CorsActiveProfile { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region CORS
            CorsActiveProfile = Configuration.GetValue<string>("Cors:ActiveProfile");
            var corsProfiles = Configuration.GetSection("Cors:Profiles")
                ?.GetChildren().Select(profile => new { 
                    Name = profile.GetSection("Name")?.Value.ToString(),
                    ExposedHeaders = profile.GetSection("ExposedHeaders")?.GetChildren().Select(exposedHeader => exposedHeader.Value)?.ToArray(),
                    Headers = profile.GetSection("Headers")?.GetChildren().Select(header => header.Value)?.ToArray(),
                    Methods = profile.GetSection("Methods")?.GetChildren().Select(method => method.Value)?.ToArray(),
                    Origins = profile.GetSection("Origins")?.GetChildren().Select(origin => origin.Value)?.ToArray()
                })
                ?.ToList();
            services.AddCors(options =>
            {
                var profile = corsProfiles.FirstOrDefault(cp => cp.Name == CorsActiveProfile);
                options.AddPolicy(name: CorsActiveProfile, builder =>
                {

                    if (profile.ExposedHeaders != null && profile.ExposedHeaders.Any())
                    {
                        builder.WithExposedHeaders(profile.ExposedHeaders);
                    }

                    if (profile.Headers != null && profile.Headers.Any())
                    {
                        builder.WithOrigins(profile.Headers);
                    }

                    if (profile.Methods != null && profile.Methods.Any())
                    {
                        builder.WithOrigins(profile.Methods);
                    }

                    if (profile.Origins != null && profile.Origins.Any())
                    {
                        builder.WithOrigins(profile.Origins);
                    }
                });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CorsActiveProfile);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
