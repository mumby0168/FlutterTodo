using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestEasy.Core.Exceptions;

namespace RestEasy.Core.Middleware
{
    public class RestEasyApiMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (InvalidGuidException e)
            {
                context.BadRequest(e.Message);
            }
            catch (InvalidRequestBodyException e)
            {
                context.BadRequest(e.Message);
            }
        }
    }
}