using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Exceptions;
using Common.Helper;
using Common.Infrastructures.MediatR;
using Common.Interfaces;
using Common.Localization;
using Common.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Cors;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using ResortAppStore.Repositories;
using Sentry;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddService(this IServiceCollection services, IConfiguration configuration,
          OpenApiInfo infoForApiVersion)
        {
            //Register dependencies
            services.RegisterDependencies(configuration);

            //Configure model state response.
            services.ConfigureInvalidModelStateResponse();

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            //Configure MVC
            services.ConfigureMvc();

            //Register Authentication
            services.RegisterAuthentication(configuration);

            //Register Swagger
            services.RegisterSwagger(infoForApiVersion);
            services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));
            //Add AutoMapper
            //  services.AddAppAutoMapper();

            //var repoInterfaces = DependenciesHelper.GetAssemblies().SelectMany(x => x.GetTypes()).ToList().Where(t => t.IsInterface && t.Name.EndsWith("Repository") && t.FullName.Contains("ResortAppStore")).ToList();
            //var repoClasses = DependenciesHelper.GetAssemblies().SelectMany(x => x.GetTypes()).ToList().Where(t => !t.IsAbstract && t.IsClass && t.Name.EndsWith("Repository") && t.FullName.Contains("ResortAppStore")).ToList();


            //repoInterfaces.ForEach(inter =>
            //{
            //    var cls = repoClasses.FirstOrDefault(x => inter.IsAssignableFrom(x));

            //    if (cls != null)
            //        services.AddScoped(inter, cls);
            //});
            //var repoInterfaces = DependenciesHelper.GetAssemblies().SelectMany(x => x.GetTypes()).ToList().Where(t => t.IsInterface && t.Name.EndsWith("GRepository") && t.FullName.Contains("")).ToList();
            //var repoClasses = DependenciesHelper.GetAssemblies().SelectMany(x => x.GetTypes()).ToList().Where(t => !t.IsAbstract && t.IsClass && t.Name.EndsWith("GRepository") && t.FullName.Contains("")).ToList();


            //repoInterfaces.ForEach(inter =>
            //{
            //    var cls = repoClasses.FirstOrDefault(x => inter.IsAssignableFrom(x));

            //    if (cls != null)
            //        services.AddScoped(inter, cls);
            //});


        }


      

        public static void AddServicePool(this IServiceCollection services,
            IConfiguration configuration, OpenApiInfo infoForApiVersion)

        {

            services.AddService(configuration, infoForApiVersion);
        }



        private static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOption:key"])),
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JwtOption:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JwtOption:Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero

                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/hup")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                context.Response.Headers.Add("Token-Expired", "true");

                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context => { return Task.CompletedTask; }
                    };
                });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("UserOnly", builder => { builder.RequireClaim("UserType", "1"); });
            //    options.AddPolicy("DriverOnly", builder => { builder.RequireClaim("UserType", "2"); });
            //    options.AddPolicy("StaffOnly", builder => { builder.RequireClaim("UserType", "3"); });
            //    options.AddPolicy("PartnerOnly", builder => { builder.RequireClaim("UserType", "4"); });
            //    options.AddPolicy("CompletedProfileOnly", builder => { builder.RequireClaim("CompletedProfile", "True"); });
            //    options.AddPolicy("SharedCustomerResource", builder =>
            //    {
            //        builder.RequireClaim("UserType", "4", "2", "1");
            //    });
            //    options.AddPolicy("PartnerOrStoreOnly", builder =>
            //    {
            //        builder.RequireClaim("UserType", "4", "5");
            //    });
            //});
        }

        private static void ConfigureInvalidModelStateResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
                apiBehaviorOptions.InvalidModelStateResponseFactory = actionContext =>
                {
                    var message = string.Empty;
                    foreach (var modelState in actionContext.ModelState.Values)
                        foreach (var error in modelState.Errors)
                            message += $" {error.ErrorMessage}.";
                    return new OkObjectResult(
                        new Result
                        {
                            Status = "Failure",
                            Message = message,
                            Response = null
                        });
                });
        }

        private static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
           
            services.AddTransient(typeof(ExceptionFilter));
            services.AddTransient(typeof(ResultFilter));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddScoped<Result>();
            services.AddScoped<IResultWrapper, StatusResultWrapper>();
            services.AddSingleton<IAuditService, AuditService>();
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            IAuditService auditServiceInstance = new AuditService(httpContextAccessor);
           // Obtain or instantiate the IAuditService implementation
            ServiceLocator.SetAuditService(auditServiceInstance);
            services.AddScoped<IEmailHelper, EmailHelper>();
             services.AddScoped(typeof(IDapperRepository<>), typeof(DapperRepository<>));

        }

        private static void ConfigureMvc(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddMvcFilter();
                // mvcOptions.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllPolicy"));
            });

            services.AddControllers(config =>
            {
                // config.Add();
                // config.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllPolicy"));
            }).AddNewtonsoftJson();

            services.AddMvc(options =>
            {

            }).AddNewtonsoftJson()
                // .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddMvcLocalization()
                .AddDataAnnotationsLocalization()
                .AddJsonOptions(options =>
                {

                    //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.JsonSerializerOptions.MaxDepth = 4326;
                    //foreach (var converter in GeoJsonSerializer.Create(new GeometryFactory(new PrecisionModel(), 4326)).Converters)
                    //{
                    //    options.SerializerSettings.Converters.Add(converter);
                    //}
                });

            services.AddCors();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllPolicy", builder =>
            //    {
            //        builder.AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .AllowAnyOrigin()
            //            .SetIsOriginAllowed(origin => true)
            //            .AllowCredentials();
            //    });
            //});

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        private static void RegisterSwagger(this IServiceCollection services,
            OpenApiInfo infoForApiVersion)
        {
            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = "Resort - " + infoForApiVersion.Title,
                        Version = description.ApiVersion.ToString()
                    });


                // options.SwaggerDoc(description.GroupName, infoForApiVersion(description));

                options.DocInclusionPredicate((docName, description) => true);
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.OperationFilter<SwaggerHeaderFilter>();
                options.AddSecurityDefinition(
                  "token",
                      new OpenApiSecurityScheme
                      {
                          Type = SecuritySchemeType.Http,
                          BearerFormat = "JWT",
                          Scheme = "Bearer",
                          In = ParameterLocation.Header,
                          Name = HeaderNames.Authorization,

                      }
                );




                options.AddSecurityRequirement(
             new OpenApiSecurityRequirement
                     {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
    }
        );


                //Determine base path for the application.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                //Set the comments path for the swagger json and ui.
                if (File.Exists(basePath + "\\DocsApi.xml"))
                    options.IncludeXmlComments(basePath + "\\DocsApi.xml");

            });
        }

        public static IContainer CreateContainer(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.ScanAndRegisterDependencies();
            containerBuilder.Populate(services);
            var autoMapperContainer = containerBuilder.Build();
            return autoMapperContainer;
        }
    }


    public class CorsAuthorizationFilterFactory : IFilterFactory, IOrderedFilter
    {
        private readonly string _policyName;

        public CorsAuthorizationFilterFactory(string policyName)
        {
            _policyName = policyName;
        }

        public int Order { get { return int.MinValue + 100; } }
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filter = serviceProvider.GetRequiredService<CorsAuthorizationFilter>();
            filter.PolicyName = _policyName;
            return filter;
        }
    }
    internal static class AutoMapperExtensions
    {
        public static void AddAppAutoMapper(this IServiceCollection services)
        {



            services.AddAutoMapper(DependenciesHelper.GetAssemblies());


        }


    }
    public static class MvcOptionsExtensions
    {
        public static void AddMvcFilter(this MvcOptions options)
        {
            AddFilters(options);
        }

        private static void AddFilters(MvcOptions options)
        {
            options.Filters.AddService(typeof(ExceptionFilter));
            options.Filters.AddService(typeof(ResultFilter));
        }
    }
    public class ExceptionFilter : IExceptionFilter//, ITransientDependency
    {

        private IWebHostEnvironment CurrentEnvironment { get; }

        public ExceptionFilter(IWebHostEnvironment env)
        {

            CurrentEnvironment = env;

        }

        public void OnException(ExceptionContext context)

        {
          if (context != null && !(context.Exception is UserFriendlyException))
                SentrySdk.CaptureException(context.Exception);

            if (context != null && !(context.ActionDescriptor is ControllerActionDescriptor))
                return;

            HandleAndWrapException(context);
        }

        protected virtual void HandleAndWrapException(ExceptionContext context)
        {
            if (!IsObjectResult(context.ActionDescriptor))
            {
                return;
            }

            context.HttpContext.Response.StatusCode = GetStatusCode(context);

            context.Result = new ObjectResult(BuildErrorResult(context.Exception));

            context.Exception = null; //Handled!
        }

        protected virtual int GetStatusCode(ExceptionContext context)
        {
            if (context.Exception is AuthorizationException)
            {
                return context.HttpContext.User.Identity.IsAuthenticated
                    ? (int)HttpStatusCode.Forbidden
                    : (int)HttpStatusCode.Unauthorized;
            }

            return (int)HttpStatusCode.BadRequest;
        }

        private bool IsObjectResult(ActionDescriptor descriptor)
        {
            if (!(descriptor is ControllerActionDescriptor controllerActionDescriptor))
                return false;

            return IsObjectReturnType(controllerActionDescriptor.MethodInfo.ReturnType);
        }

        private bool IsObjectReturnType(Type returnType)
        {
            if (returnType == typeof(Task))
            {
                returnType = typeof(void);
            }
            else if (returnType.GetTypeInfo().IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                returnType = returnType.GenericTypeArguments[0];
            }

            if (typeof(IActionResult).GetTypeInfo().IsAssignableFrom(returnType))
            {
                if (typeof(JsonResult).GetTypeInfo().IsAssignableFrom(returnType) || typeof(ObjectResult).GetTypeInfo().IsAssignableFrom(returnType))
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        private Result BuildErrorResult(Exception exception)
        {
            if (exception is AggregateException && exception.InnerException != null)
            {
                return ResultHelper.CreateFailure(exception.InnerException.Message);
            }

            if (exception is UserFriendlyException userFriendlyException)
            {
                return ResultHelper.CreateFailure(userFriendlyException.Message);
            }
            var exceptionMsg = exception.GetFullExceptionMessage();

            //var msg = "something went wrong";
            //if (CurrentEnvironment.IsProduction())
            //    return ExceptionResultHelper.CreateFailure(msg, exceptionMessage: exceptionMsg);
            return ExceptionResultHelper.CreateFailure(exceptionMsg, exceptionMessage: exceptionMsg);

            //#if DEBUG
            // TODO: delete the full message and enable the internal server error.
            //#endif
            //         return StatusResultHelper.CreateFailure("InternalServerError");
        }
    }
    public static class GenericExtensions
    {
       

        public static string GetFullExceptionMessage(this Exception exception)
        {
            var result = exception.Message;
            var inner = exception.InnerException;

            while (inner != null)
            {
                result += "\n" + inner.Message;

                inner = inner.InnerException;
            }

            return result;
        }
    }
    public class ResultFilter : IResultFilter//, ITransientDependency
    {
        protected IResultWrapper ResultWrapper { get; }

        public ResultFilter(IResultWrapper resultWrapper)
        {
            ResultWrapper = resultWrapper;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!ShouldWrapResult(context))
                return;

            if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                badRequestObjectResult.Value = ResultWrapper.WrapException(badRequestObjectResult.Value, context.ActionDescriptor);
                badRequestObjectResult.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Result is ObjectResult objectResult)
            {
                objectResult.Value = ResultWrapper.WrapIt(objectResult.Value, context.ActionDescriptor);
                objectResult.StatusCode = ResultWrapper.StatusCode;
            }
            else if (context.Result is EmptyResult emptyResult)
            {
                var result = new ObjectResult(null)
                {
                    Value = ResultWrapper.WrapIt(null, context.ActionDescriptor),
                    StatusCode = ResultWrapper.StatusCode
                };

                context.Result = result;
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public bool ShouldWrapResult(ResultExecutingContext context)
        {
            //Check if action has DoNotWrapResult attribute
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true);
                if (actionAttributes != null && actionAttributes.Any())
                {
                    if (actionAttributes.OfType<DoNotWrapResultAttribute>().Any())
                    {
                        return false;
                    }
                }
            }

            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult.Value == null)
                    return true;

                // Is already wrapped
                if (objectResult.Value is Result ||
                    (objectResult.Value.GetType().IsGenericType &&
                     objectResult.Value.GetType().GetGenericTypeDefinition() == typeof(Result<>)))
                    return false;
            }

            return true;
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DoNotWrapResultAttribute : Attribute
    {

    }
    public interface IResultWrapper// : ITransientDependency
    {
        object WrapIt(object result, ActionDescriptor actionDescriptor);
        int StatusCode { get; }
        object WrapException(object result, ActionDescriptor actionDescriptor);
    }
}



