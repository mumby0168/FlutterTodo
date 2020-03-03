using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestEasy.Core.Markers;

namespace RestEasy.Core.Handlers.Generic
{
    public interface IRestEasyPostHandler<TDomain, in TDto> where TDomain : IDomain<TDto> where TDto : IDto
    {
        Task HandleAsync(TDto dto, HttpContext context);
    }
}