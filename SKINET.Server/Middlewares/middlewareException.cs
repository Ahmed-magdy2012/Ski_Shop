using SKINET.Server.Controllers;
using SKINET.Server.Errors;
using System.Net;
using System.Text.Json;

namespace SKINET.Server.Middlewares
{
    public class middlewareException(IHostEnvironment env,RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e) { 

             await Handelexception(context,e,env);
            }
        }

        private  Task Handelexception(HttpContext context, Exception e, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var res = env.IsDevelopment() ?
                new ApiErrorResponse(context.Response.StatusCode, e.Message, e.StackTrace)
                : new ApiErrorResponse(context.Response.StatusCode, e.Message, "Internal server error");
            var json = JsonSerializer.Serialize(res);
            return context.Response.WriteAsync(json);

        }
    }
}
