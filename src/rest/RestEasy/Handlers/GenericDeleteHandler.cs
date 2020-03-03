using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestEasy.Core.Factories;
using RestEasy.Core.Handlers;
using RestEasy.Core.Handlers.Generic;
using RestEasy.Core.Markers;
using RestEasy.Core.Persistence;

namespace RestEasy.Handlers
{
    public class GenericDeleteHandler<TDomain, TDto> : IDeleteHandler<TDomain, TDto> where TDomain : IDomain<TDto>, new() where TDto : IDto
    {
        private readonly IRepository<TDomain, TDto> _repository;

        public GenericDeleteHandler(IRestEasyRepositoryFactory factory)
        {
            _repository = factory.ResolveRepository<TDomain, TDto>();
        }
        
        public async Task HandleAsync(Guid id, HttpContext context)
        {
            var domain = await _repository.GetAsync(id);

            if (domain is null)
            {
                context.BadRequest($"The domain with id: {id} could not be found.");
                return;
            }

            await _repository.RemoveAsync(domain);
            context.Ok();
        }
    }
}