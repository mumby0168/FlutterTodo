using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestEasy.Core.Markers;

namespace RestEasy.Core.Handlers.Generic
{
    public interface IGetHandler<TDomain, TDto> where TDomain : IDomain<TDto> where TDto : IDto
    {
        Task GetAsync(Guid id, HttpContext context);
        
        Task GetAllAsync(HttpContext context);
    }
}