using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEasy.Core.Markers;

namespace RestEasy.Core.Persistence
{
    public interface IRepository<TDomain, TDto> where TDomain : IDomain<TDto>, new() where TDto : IDto
    {
        Task AddAsync(TDomain domain);

        Task UpdateAsync(TDomain domain);

        Task<TDomain> GetAsync(Guid id);

        Task<IEnumerable<TDomain>> GetAllAsync();

        Task RemoveAsync(TDomain domain);
    }
}