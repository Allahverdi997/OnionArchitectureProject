using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RusMProjectApplication.Abstraction.Services;
using RusMProjectApplication.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RusMProject.WebAPI.Filters
{
    public class UnhandledExceptionFilter : IExceptionFilter
    {
        private readonly IExceptionNotificationServiceAble ExceptionNotificationService;
        public UnhandledExceptionFilter(IExceptionNotificationServiceAble exceptionNotificationService)
        {
            ExceptionNotificationService = exceptionNotificationService;
        }
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType().IsSubclassOf(typeof(CustomException)))
            {
                var exception =(CustomException)context.Exception;
                var name = exception.Name;

                context.HttpContext.Response.WriteAsync($"Name:{exception.Name},Exception:{exception.Message}").Wait();
            }
            else
            {
                context.HttpContext.Response.WriteAsync($"Exception:{context.Exception.Message}").Wait();
            }
            
        }
    }
}
