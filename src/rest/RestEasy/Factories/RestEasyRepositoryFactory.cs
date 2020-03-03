using System;
using Microsoft.Extensions.DependencyInjection;
using RestEasy.Core.Exceptions;
using RestEasy.Core.Factories;
using RestEasy.Core.Markers;
using RestEasy.Core.Persistence;

namespace RestEasy.Factories
{
    public class RestEasyRepositoryFactory : IRestEasyRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RestEasyRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public IRepository<TDomain, TDto> ResolveRepository<TDomain, TDto>() where TDomain : IDomain<TDto>, new() where TDto : IDto
        {
            var repo = _serviceProvider.GetService<IRepository<TDomain, TDto>>();

            if (repo is null)
            {
                throw new RestEasyResolveException();
            }

            return repo;
        }
    }
}