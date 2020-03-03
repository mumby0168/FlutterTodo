


using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using RestEasy.Core.Factories;
using RestEasy.Core.Handlers;
using RestEasy.Core.Handlers.Generic;
using RestEasy.Core.Markers;
using RestEasy.Core.Middleware;

namespace RestEasy
{
    public static class RestEasyAppExtensions
    {
        public static IEndpointRouteBuilder MapRestEasyApi<TDomain, TDto>(this IEndpointRouteBuilder builder, string basePath) where TDto : IDto where TDomain : IDomain<TDto>
        {
            builder.MapPost(basePath, async context =>
            {
                var genericPost = builder.ServiceProvider.GetService<IRestEasyPostHandler<TDomain, TDto>>();
                var dto = await context.Request.ReadAsJsonAsync<TDto>();
                await genericPost.HandleAsync(dto, context);
            });

            var get = builder.ServiceProvider.GetService<IGetHandler<TDomain, TDto>>();
            builder.MapGet(basePath + "/{id}", async context =>
            {
                var guid = context.Request.ExtractGuidFromRoute();
                await get.GetAsync(guid, context);
            });

            builder.MapGet(basePath, async context => await get.GetAllAsync(context));
            
            builder.MapPut(basePath, async context =>
            {
                var put = builder.ServiceProvider.GetService<IPutHandler<TDomain, TDto>>();
                var dto = await context.Request.ReadAsJsonAsync<TDto>();
                await put.HandleAsync(dto, context);
            });
            
            builder.MapDelete(basePath + "/{id}", async context =>
            {
                var delete = builder.ServiceProvider.GetService<IDeleteHandler<TDomain, TDto>>();
                var id = context.Request.ExtractGuidFromRoute();
                await delete.HandleAsync(id, context);
            });
            return builder;
        }

        /// <summary>
        /// Maps a basic endpoint for a get taking a id.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="path"></param>
        /// <typeparam name="TReturns">The value that the request will return</typeparam>
        /// <typeparam name="TInput">This should be a int or a Guid</typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public static IEndpointRouteBuilder Get<TReturns>(this IEndpointRouteBuilder builder, string path)
            where TReturns : class
        {
            return builder;
        }

        public static IEndpointRouteBuilder Get<TReturns, TInput>(this IEndpointRouteBuilder builder, string path)
            where TReturns : class
        {
            return builder;
        }

        public static IApplicationBuilder UseRestEasy(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<RestEasyApiMiddleware>();
            return builder;
        }
        
    }
}