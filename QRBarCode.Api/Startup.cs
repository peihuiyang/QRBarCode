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
        /// �����б�
        /// </summary>
        private static readonly List<string> _Assemblies = new List<string>()
        {
            "QRBarCode.DomainServer"
        };
        /// <summary>
        /// ����ע��
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

            // ͨ��Autofac�Զ��������ע��
            container.RegisterTypes(allTypes.ToArray())
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency();

            // ע��Controller
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
                   //�޸��������Ƶ����л���ʽ������ĸСд
                   options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                   //�޸�ʱ������л���ʽ
                   //options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
                   //options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                   options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                           });            //ע��http�������
            services.AddHttpClient();

            #region Swagger������
            services.AddSwaggerGen(c =>
            {
                // => ����v1
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "�������ά�����ɷ���Api",
                    Version = "v1",
                    Description = "�ܸ�����������������Ͷ�ά��",
                    TermsOfService = new Uri("https://github.com/peihuiyang/"),
                    Contact = new OpenApiContact
                    {
                        Name = "�����",
                        Email = "2019070053@sanhepile.com",
                        Url = new Uri("https://mail.qq.com/")
                    }
                });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Token��¼��֤,��ʽ��Bearer {token}(ע������֮����һ���ո�)",
                    Name = "Authorization",
                    //���������������޸�
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
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "QRBarCode.Api.xml");
                c.IncludeXmlComments(xmlPath);//�ڶ�������true��ʾ�ÿ�������XMLע�͡�Ĭ��false
                //��ӶԿ������ı�ǩ(����)
                c.DocumentFilter<SwaggerDocTag>();
                //c.OperationFilter<SwaggerFileUploadFilter>();
            });
            #endregion

            #region ȫ���쳣����
            // =>ע��MVC��Container,ͬʱ���ȫ�ֹ�����
            services.AddMvcCore(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Swagger�м������
            //�����м����������Swagger��ΪJSON�ս��
            app.UseSwagger();
            //�����м�������swagger-ui��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "�������ά�����ɷ���Api V1");
                c.RoutePrefix = string.Empty;
            });
            #endregion           

            //����֧��nginx
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
