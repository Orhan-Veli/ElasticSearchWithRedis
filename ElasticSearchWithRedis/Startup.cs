using ElasticSearchWithRedis.Business.Abstract;
using ElasticSearchWithRedis.Business.Concrete;
using ElasticSearchWithRedis.Dal.Abstract;
using ElasticSearchWithRedis.Dal.Concrete;
using ElasticSearchWithRedis.Dal.Entity;
using ElasticSearchWithRedis.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElasticSearchWithRedis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
     
        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddSingleton(_configuration);
            services.AddControllers();            
            services.AddScoped<IElasticSearchService,ElasticSearchService>();           
            services.AddSingleton<IElasticRepository<MachineConnectionInformation>, ElasticRepository>();
            services.AddSingleton<IElasticService, ElasticService>();           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
