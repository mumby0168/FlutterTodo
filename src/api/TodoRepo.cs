using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEasy.Core.Persistence;
using Microsoft.Extensions.Logging;

namespace api
{
    public class TodoRepo : IRepository<TodoItem, TodoItemDto>
    {
        private readonly ILogger<TodoRepo> logger;
        public TodoRepo(ILogger<TodoRepo> logger)
        {
            this.logger = logger;
            if (!Startup.Items.Any())
            {
                var item = new TodoItem();
                item.Map(new TodoItemDto { Text = "Task from the api" }, true);
                Startup.Items.Add(item);
            }
        }
        public Task AddAsync(TodoItem domain)
        {
            logger.LogInformation($"Item added with text: {domain.Text}");
            Startup.Items.Add(domain);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TodoItem>> GetAllAsync() {
            logger.LogInformation("Request made");
            return Task.FromResult(Startup.Items.AsEnumerable());}


        public Task<TodoItem> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(TodoItem domain)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TodoItem domain)
        {
            throw new NotImplementedException();
        }
    }
}