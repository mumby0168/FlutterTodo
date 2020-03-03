using System;
using RestEasy.Core.Markers;

namespace api
{
    public class TodoItemDto : IDto
    {
        public Guid Id {get;set;}

        public string Text {get;set;}
    }
}