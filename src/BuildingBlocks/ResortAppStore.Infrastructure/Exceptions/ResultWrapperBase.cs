using Common.Extensions;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.Localization;
using Microsoft.Extensions.Localization;
using Serilog;
using Common.Entity;
using Microsoft.AspNetCore.Hosting;

namespace Common.Exceptions
{
    public abstract class ResultWrapperBase : IResultWrapper
    {
        public abstract object WrapIt(object result, ActionDescriptor actionDescriptor);

        public virtual int StatusCode => (int)HttpStatusCode.OK;
        public abstract object WrapException(object result, ActionDescriptor actionDescriptor);
    }
    public class StatusResultWrapper : ResultWrapperBase
    {
       
        private readonly IAuditService _appSession;
        private readonly IStringLocalizer<StatusResultWrapper> _stringLocalizer;
        public StatusResultWrapper( IAuditService appSession, IStringLocalizer<StatusResultWrapper> stringLocalizer)
        {
           // _localizationProvider = localizationProvider;
            _appSession = appSession;
            _stringLocalizer = stringLocalizer;
        }

        public override object WrapIt(object result, ActionDescriptor actionDescriptor)
        {
            var statusResult = new Result
            {
                Status = "Success",
                Response = result,
                Message = GetSuccessMessage(actionDescriptor),
                Success=true
            };

            return statusResult;
        }

        public override object WrapException(object result, ActionDescriptor actionDescriptor)
        {
            var statusResult = new Result
            {
                Status = "Failure",
                Response = result,
                Message = string.Empty,
                Success=false
            };

            return statusResult;
        }

        protected virtual string GetSuccessMessage(ActionDescriptor actionDescriptor)
        {
            //Check if action has DoNotWrapResult attribute
            if (!(actionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
                return string.Empty;

            var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true);
            if (actionAttributes == null || !actionAttributes.Any())
                return string.Empty;

            var attribute = new SuccessResultMessageAttribute();
            foreach (var actionAttribute in actionAttributes)
            {
                if (!(actionAttribute is SuccessResultMessageAttribute messageAttribute))
                    continue;

                attribute = messageAttribute;
                break;
            }
          
           
            return string.IsNullOrWhiteSpace(attribute.Message)
                ? string.Empty
                : _stringLocalizer[attribute.Message, attribute.ServicePath];
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SuccessResultMessageAttribute : Attribute
    {
        public string Message { get; set; }
        public string ServicePath { get; set; } 

        public SuccessResultMessageAttribute(string message)
        {
            Message = message;
        }
        public SuccessResultMessageAttribute(string message, string servicePath)
        {
            Message = message;
            ServicePath = servicePath;
        }
        public SuccessResultMessageAttribute()
        {

        }
    }
}
