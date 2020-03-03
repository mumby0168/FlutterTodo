using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using RestEasy.Core.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RestEasy
{
    public static class HttpContextExtensions
    {
        public static async void Ok(this HttpContext context, string message = null)
        {
            context.Response.StatusCode = (int) HttpStatusCode.OK;
            if (message != null)
            {
                await context.Response.WriteAsync(message);
            }
        }

        public static async void Ok<T>(this HttpContext context, T obj)
        {
            context.Response.StatusCode = (int) HttpStatusCode.OK;
            var result = JsonSerializer.Serialize(obj);
            await context.Response.WriteAsync(result);
        }
        
        public static async void NoContent<T>(this HttpContext context, T obj)
        {
            context.Response.StatusCode = (int) HttpStatusCode.NoContent;
            var result = JsonSerializer.Serialize(obj);
            await context.Response.WriteAsync(result);
        }
        
        public static async void BadRequest(this HttpContext context, string message = null)
        {
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            if (message != null)
            {
                await context.Response.WriteAsync(message);
            }
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpRequest request)
        {
            if (request.Body is null)
            {
                throw new InvalidRequestBodyException("The request body was empty please place a dto.");
            }

            using var reader = new StreamReader(request.Body);
            
            var str = await reader.ReadToEndAsync();
            
            
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static Guid ExtractGuidFromRoute(this HttpRequest request)
        {
            var routeData = request.HttpContext.GetRouteData();
            if (routeData.Values.Keys.Contains("id"))
            {
                var guidString = routeData.Values["id"];
                return Guid.TryParse(guidString.ToString(), out Guid result)
                    ? result
                    : throw new InvalidGuidException("The Guid could not be converted please check.");
            }
            else
            {
                throw new InvalidGuidException("The Guid was not present as a route parameter.");
            }
        }


        public static void NoContent(this HttpContext context)
        {
            context.Response.StatusCode = (int) HttpStatusCode.NoContent;
        }
    }
}