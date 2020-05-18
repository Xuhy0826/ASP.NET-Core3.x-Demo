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
            #region ��Ŀ����
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
                            Title = "�д��󣡣���",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "�뿴��ϸ��Ϣ",
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
                setup.SwaggerDoc("DemoApi",        //�����ĵ�������
                    new OpenApiInfo
                    {
                        Title = "DemoApi",
                        Version = "v1.0",
                        Contact = new OpenApiContact
                        {
                            Name = "xuhy",
                            Email = "hyxu0826@gmail.com"
                        },
                        Description = "Demo Api ��������Ϣ"
                    });

                //�ڽ�������ʾ��Ŀ�е�ע��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var commentsFileNameMain = typeof(Program).Assembly.GetName().Name + ".XML";
                setup.IncludeXmlComments(Path.Combine(basePath, commentsFileNameMain));
                setup.DocInclusionPredicate((docName, description) => true);
                //ע�������
                setup.OperationFilter<SwaggerExcludePropFilter>();
            });
            //����Ŀʹ�õ���Newtonsoft,���Բ���Ҫ����ķ���
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

            // ����Swagger�м��
            app.UseSwagger();
            // ����SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/DemoApi/swagger.json", "DemoApi��Ŀ�ĵ�");
                c.RoutePrefix = string.Empty;

            });
        }
    }
}
