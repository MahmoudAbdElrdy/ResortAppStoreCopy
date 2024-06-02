using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class DependenciesHelper
    {
        public static void ScanAndRegisterDependencies(this ContainerBuilder containerBuilder)
        {
            var assemblies = GetAssemblies();
            foreach (var assembly in assemblies)
            {
                RegisterSingltoneDependency(containerBuilder, assembly);
                RegisterTransientDependency(containerBuilder, assembly);
                RegisterScopedDependency(containerBuilder, assembly);
                RegisterAutoMapperDependency(containerBuilder, assembly);
            }
        }

        private static void RegisterAutoMapperDependency(ContainerBuilder containerBuilder, Assembly assembly)
        {

            containerBuilder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsClass && t.IsPublic &&
                            t.IsSubclassOf(typeof(Profile)))
                .AsSelf()
                .AssignableTo<Profile>()
                .InstancePerLifetimeScope();
        }

        private static void RegisterTransientDependency(ContainerBuilder containerBuilder, Assembly assembly)
        {
            containerBuilder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsClass && t.IsPublic &&
                            t.GetInterfaces().Contains(typeof(ITransientDependency)))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        private static void RegisterScopedDependency(ContainerBuilder containerBuilder, Assembly assembly)
        {
            containerBuilder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsClass && t.IsPublic &&
                            t.GetInterfaces().Contains(typeof(IScopedDependency)))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private static void RegisterSingltoneDependency(ContainerBuilder containerBuilder, Assembly assembly)
        {
            containerBuilder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsClass && t.IsPublic &&
                            t.GetInterfaces().Contains(typeof(ISingletonDependency)))
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        public static List<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }



        public static IHttpContextAccessor GetHttpContextAccessor()
        {
            var builder = new ContainerBuilder();


            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            builder.RegisterType<Microsoft.Extensions.Configuration.ConfigurationRoot>().As<IConfiguration>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                return scope.ResolveOptional<IHttpContextAccessor>();
            }
        }
    }
    public interface ITransientDependency
    {

    }
    public interface IScopedDependency
    {

    }
    public interface ISingletonDependency
    {

    }
    public class SwaggerHeaderFilter : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null && descriptor.ControllerName != null)
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "Language",
                    In = ParameterLocation.Header,

                    Schema = new OpenApiSchema
                    {
                        Type = "String",
                        Default = new OpenApiString("en-US")
                    },
                    Required = false
                });
                if (descriptor.DisplayName.Contains("Nashmi.Services.Identity.API.Controllers"))
                {
                    operation.Parameters.Add(new OpenApiParameter()
                    {
                        Name = "CountryId",
                        In = ParameterLocation.Header,

                        Schema = new OpenApiSchema
                        {
                            Type = "int",
                            Default = new OpenApiString("2")

                        },
                        Required = false
                    });
                }

                if (descriptor.DisplayName.Contains("Nashmi.Services.Identity.API.Controllers.V3_2"))
                {
                    operation.Parameters.Add(new OpenApiParameter()
                    {
                        Name = "ShgardiKey",
                        In = ParameterLocation.Header,

                        Schema = new OpenApiSchema
                        {
                            Type = "String",
                            Default = new OpenApiString("")

                        },
                        Required = false
                    });
                }
            }
        }
    }
}
