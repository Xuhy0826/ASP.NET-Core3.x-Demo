using System;
using System.IO;
using System.Linq;
using ASP.NET_Core3.x_WebApi_Demo.Data;
using ASP.NET_Core3.x_WebApi_Demo.Repository;
using ASP.NET_Core3.x_WebApi_Demo.Services;
using ASP.NET_Core3.x_WebApi_Demo.SwaggerFilter;
using AutoMapper;
using Mark.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace ASP.NET_Core3.x_WebApi_Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region 项目所需
            services.AddControllers().AddNewtonsoftJson(setup =>
                {
                    setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setup =>
                {
                    setup.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "http://www.baidu.com",
                            Title = "有错误！！！",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "请看详细信息",
                            Instance = context.HttpContext.Request.Path
                        };

                        problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });
            services.Configure<MvcOptions>(config =>
            {
                var newtonSoftJsonOutputFormatter =
                    config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

                newtonSoftJsonOutputFormatter?.SupportedMediaTypes.Add("application/vnd.demo.hateoas+json");
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddDbContext<DemoDbContext>(option =>
            {
                option.UseSqlite("Data Source=demo.db");
            });
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();
            #endregion

            #region Swagger

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("DemoApi",        //定义文档的名称
                    new OpenApiInfo
                    {
                        Title = "DemoApi",
                        Version = "v1.0",
                        Contact = new OpenApiContact
                        {
                            Name = "xuhy",
                            Email = "hyxu0826@gmail.com"
                        },
                        Description = "Demo Api 的描述信息"
                    });

                //在界面中显示项目中的注释
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var commentsFileNameMain = typeof(Program).Assembly.GetName().Name + ".XML";
                setup.IncludeXmlComments(Path.Combine(basePath, commentsFileNameMain));
                setup.DocInclusionPredicate((docName, description) => true);
                //注册过滤器
                setup.OperationFilter<SwaggerExcludePropFilter>();
            });
            //本项目使用的是Newtonsoft,所以不需要下面的方法
            //services.AddSwaggerGenNewtonsoftSupport(); 
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unexpected Error!");
                    });
                });
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // 启用Swagger中间件
            app.UseSwagger();
            // 配置SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/DemoApi/swagger.json", "DemoApi项目文档");
                c.RoutePrefix = string.Empty;

            });
        }
    }
}
