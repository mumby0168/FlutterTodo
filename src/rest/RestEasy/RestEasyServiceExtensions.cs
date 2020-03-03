using Microsoft.Extensions.DependencyInjection;
using RestEasy.Core.Factories;
using RestEasy.Core.Handlers;
using RestEasy.Core.Handlers.Generic;
using RestEasy.Core.Markers;
using RestEasy.Core.Middleware;
using RestEasy.Core.Persistence;
using RestEasy.Factories;
using RestEasy.Handlers;

namespace RestEasy
{
    public static class RestEasyServiceExtensions
    {
        public static IServiceCollection AddRestEasy(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRestEasyApiFactory, RestEastApiFactory>();
            serviceCollection.AddTransient<IRestEasyRepositoryFactory, RestEasyRepositoryFactory>();
            return serviceCollection;
        }

        public static IServiceCollection AddRestEasyApi<TDomain, TDto, TRepo>(this IServiceCollection services)
            where TRepo : class, IRepository<TDomain, TDto> where TDto : IDto, new() where TDomain : IDomain<TDto>, new()
        {

            services.AddTransient<RestEasyApiMiddleware>();
            services.AddTransient<IRepository<TDomain, TDto>, TRepo>();
            services.AddTransient<IRestEasyPostHandler<TDomain, TDto>, GenericRestEasyPostHandler<TDomain, TDto>>();
            services.AddTransient<IGetHandler<TDomain, TDto>, GenericGetHandler<TDomain, TDto>>();
            services.AddTransient<IPutHandler<TDomain, TDto>, GenericPutHandler<TDomain, TDto>>();
            services.AddTransient<IDeleteHandler<TDomain, TDto>, GenericDeleteHandler<TDomain, TDto>>();
            return services;
        }
    }
}