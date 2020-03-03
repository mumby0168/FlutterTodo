using System;
using RestEasy.Core.Markers;

namespace api
{
    public class TodoItem : IDomain<TodoItemDto>
    {
        public TodoItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id {get; private set;}

        public string Text {get; private set;}

        public void Map(TodoItemDto dto, bool firstCreation = false)
        {
            if(!firstCreation) Id = dto.Id;
            Text = dto.Text;
        }

        public TodoItemDto Map()
        {
            return new TodoItemDto{Id = Id, Text = Text};
        }
    }
}