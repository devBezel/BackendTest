using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PumoxRecruitmentTask.API.AutoMapperConfig;
using PumoxRecruitmentTask.BLL.Interfaces.Services;
using PumoxRecruitmentTask.BLL.Services;
using PumoxRecruitmentTask.DAL.DataAccess;
using PumoxRecruitmentTask.DAL.Migrations;
using PumoxRecruitmentTask.DAL.UnitOfWork;

namespace PumoxRecruitmentTask.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddDbContext<DataContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("PumoxRecruitmentTaskDbString"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddAutoMapper(cfg => cfg.AddProfile<DtoProfile>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(cfg =>
            {
                cfg.Run(async ctx =>
                {
                    ctx.Response.ContentType = "application/json";
                });
            });

            app.UseHsts();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
          
            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            var migrationService = new MigrationService(Configuration.GetConnectionString("PumoxRecruitmentTaskDbString"));
            migrationService.UpdateDatabase();
        }
    }
}