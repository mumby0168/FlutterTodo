using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestEasy.Core.Handlers;
using RestEasy.Core.Handlers.Generic;
using RestEasy.Core.Markers;
using RestEasy.Core.Persistence;

namespace RestEasy.Handlers
{
    public class GenericGetHandler<TDomain, TDto> : IGetHandler<TDomain, TDto> where TDomain : IDomain<TDto>, new() where TDto : IDto, new()
    {
        private readonly IRepository<TDomain, TDto> _repository;

        public GenericGetHandler(IRepository<TDomain, TDto> repository)
        {
            _repository = repository;
        }
        

        public async Task GetAsync(Guid id, HttpContext context)
        {
            var domain = await _repository.GetAsync(id);
            if (domain is null)
            {
                context.BadRequest($"The domain with id: {id} could not be found.");
                return;
            }
            var dto = domain.Map();
            context.Ok(dto);
        }

        public async Task GetAllAsync(HttpContext context)
        {
            var domains = await _repository.GetAllAsync();
            var dtos = domains.Select(domain => domain.Map()).ToList();
            if (dtos.Any())
            {
                context.Ok(dtos);
            }
            else
            {
                context.NoContent();
            }
            
        }
    }
}