using ElasticSearchWithRedis.Business.Abstract;
using ElasticSearchWithRedis.Business.Concrete;
using ElasticSearchWithRedis.Dal.Abstract;
using ElasticSearchWithRedis.Dal.Concrete;
using ElasticSearchWithRedis.Dal.Entity;
using ElasticSearchWithRedis.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using System;

namespace ElasticSearchWithRedis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public IConfiguration _configuration { get; }
        public IElasticClient _client { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddSingleton(_configuration);
            services.AddControllers();
            services.AddSingleton<IElasticRepository<MachineConnectionInformation>, ElasticRepository>();
            services.AddSingleton<IElasticClient>(
            p =>
            {
                var elas = new ElasticClient(new ConnectionSettings(new Uri(_configuration["elasticsearchserver:Host"]))
              .BasicAuthentication(_configuration["elasticsearchserver:Username"], _configuration["elasticsearchserver:Password"]));
                var anyy = elas.Indices.Exists(_configuration["elasticsearchserver:indexName"].ToString());
                if (!anyy.Exists)
                {
                    elas.Indices.Create(_configuration["elasticsearchserver:indexName"].ToString(), ci => ci.Index(_configuration["elasticsearchserver:indexName"].ToString()).MachineMapping().Settings(s => s.NumberOfShards(3).NumberOfReplicas(1)));
                }
                return elas;
            });
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
