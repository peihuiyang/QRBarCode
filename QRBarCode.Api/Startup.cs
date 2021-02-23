using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using QRBarCode.Api.Configuration;
using QRBarCode.Api.Configuration.Filters;
using QRBarCode.Api.Configuration.Swagger;

namespace QRBarCode.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 程序集列表
        /// </summary>
        private static readonly List<string> _Assemblies = new List<string>()
        {
            "QRBarCode.DomainServer"
        };
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="container"></param>
        public void ConfigureContainer(ContainerBuilder container)
        {
            var assemblys = _Assemblies.Select(x => Assembly.Load(x)).ToList();
            List<Type> allTypes = new List<Type>();
            assemblys.ForEach(aAssembly =>
            {
                allTypes.AddRange(aAssembly.GetTypes());
            });

            // 通过Autofac自动完成依赖注入
            container.RegisterTypes(allTypes.ToArray())
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency();

            // 注册Controller
            container.RegisterAssemblyTypes(typeof(Startup).GetTypeInfo().Assembly)
                .Where(t => typeof(Controller).IsAssignableFrom(t) && t.Name.EndsWith("Controller", StringComparison.Ordinal))
                .PropertiesAutowired();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()))
                           .AddNewtonsoftJson(options =>
                           {
                   //修改属性名称的序列化方式，首字母小写
                   options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                   //修改时间的序列化方式
                   //options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
                   //options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                   options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                           });            //注册http请求服务
            services.AddHttpClient();

            #region Swagger生成器
            services.AddSwaggerGen(c =>
            {
                // => 分组v1
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "条形码二维码生成服务Api",
                    Version = "v1",
                    Description = "能根据需求生成条形码和二维码",
                    TermsOfService = new Uri("https://github.com/peihuiyang/"),
                    Contact = new OpenApiContact
                    {
                        Name = "杨培辉",
                        Email = "2019070053@sanhepile.com",
                        Url = new Uri("https://mail.qq.com/")
                    }
                });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Token登录验证,格式：Bearer {token}(注意两者之间是一个空格)",
                    Name = "Authorization",
                    //这两个参数均有修改
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                     {
                          new OpenApiSecurityScheme
                          {
                                Reference = new OpenApiReference
                                {
                                      Type = ReferenceType.SecurityScheme,
                                      Id = "Bearer"
                                }
                          },
                          new string[] { }
                     }
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "QRBarCode.Api.xml");
                c.IncludeXmlComments(xmlPath);//第二个参数true表示用控制器的XML注释。默认false
                //添加对控制器的标签(描述)
                c.DocumentFilter<SwaggerDocTag>();
                //c.OperationFilter<SwaggerFileUploadFilter>();
            });
            #endregion

            #region 全局异常捕获
            // =>注册MVC到Container,同时添加全局过滤器
            services.AddMvcCore(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Swagger中间件服务
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "条形码二维码生成服务Api V1");
                c.RoutePrefix = string.Empty;
            });
            #endregion           

            //配置支持nginx
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
