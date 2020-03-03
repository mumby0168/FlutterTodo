using RestEasy.Core.Markers;
using RestEasy.Core.Persistence;

namespace RestEasy.Core.Factories
{
    public interface IRestEasyRepositoryFactory
    {
        IRepository<TDomain, TDto> ResolveRepository<TDomain, TDto>() where TDomain : IDomain<TDto>, new() where TDto : IDto;
    }
}    