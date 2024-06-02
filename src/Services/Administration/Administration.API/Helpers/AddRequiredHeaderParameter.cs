﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
namespace Administration.Helpers
{
    public class AddRequiredHeaderParameter : IOperationFilter
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
                   //     Default = new OpenApiString("en")
                        Default = new OpenApiString("ar")
                    },
                    Required = false
                });
              
            }
        }
    }
}
