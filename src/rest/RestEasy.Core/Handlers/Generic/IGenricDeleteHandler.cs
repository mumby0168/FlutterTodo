using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestEasy.Core.Markers;

namespace RestEasy.Core.Handlers.Generic
{
    public interface IDeleteHandler<TDomain, TDto> where TDomain : IDomain<TDto> where TDto : IDto
    {
        Task HandleAsync(Guid id, HttpContext context);
    }
}