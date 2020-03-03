using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestEasy.Core.Factories;
using RestEasy.Core.Handlers;
using RestEasy.Core.Handlers.Generic;
using RestEasy.Core.Markers;
using RestEasy.Core.Persistence;

namespace RestEasy.Handlers
{
    public class GenericPutHandler<TDomain, TDto> : IPutHandler<TDomain, TDto> where TDomain : IDomain<TDto>, new() where TDto : IDto
    {
        private readonly IRepository<TDomain, TDto> _repository;

        public GenericPutHandler(IRestEasyRepositoryFactory factory)
        {
            _repository = factory.ResolveRepository<TDomain, TDto>();
        }
        
        
        public async Task HandleAsync(TDto dto, HttpContext context)
        {
            if (dto is null)
            {
                context.BadRequest("The dto was null");
                return;
            }
            
            var domain = await _repository.GetAsync(dto.Id);
            
            if (domain is null)
            {
                context.BadRequest($"The domain object with id: {dto.Id} could not be found.");
                return;
            }

            domain.Map(dto);
            
            await _repository.UpdateAsync(domain);
            
            context.Ok();
        }
    }
}
