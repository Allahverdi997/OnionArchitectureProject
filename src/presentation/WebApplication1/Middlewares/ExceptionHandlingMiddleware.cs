using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RusMProjectApplication.Abstraction.Services;
using RusMProjectApplication.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RusMProject.WebAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        public IExceptionNotificationServiceAble ExceptionNotificationService { get; set; }
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (ex.GetType().IsSubclassOf(typeof(CustomException)))
                {
                    var exception = (CustomException)ex;
                    var name = exception.Name;

                    var content = new { Key = name, Content = ExceptionNotificationService.Get(name), ExceptionMessage = ex.InnerException.Message };
                    var response = JsonConvert.SerializeObject(content);
                    await context.Response.WriteAsync(response);
                }
            }
        }
    }
}
