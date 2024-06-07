﻿namespace TodoListApp.WebApi.Models // Or your preferred namespace
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection; // Add this for dependency injection
    using Microsoft.Extensions.Logging; // Add this for logging
    using TodoListApp.WebApi.DataContext;
    using TodoListApp.WebApi.Models.Tasks;

    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<TodoListDbContext>();
            try
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.TodoLists.Any()) // Check if data already exists
                {
                    var todoLists = new List<TodoListEntity>
                    {
                        new TodoListEntity
                        {
                            Name = "Shopping List",
                            Description = "Things to buy at the store",
                            Tasks =
                            {
                                new TaskEntity { Title = "Bread", DueDate = DateTime.Today },
                                new TaskEntity { Title = "Milk", DueDate = DateTime.Today },
                                new TaskEntity { Title = "Eggs", DueDate = DateTime.Today },
                                new TaskEntity { Title = "Cheese", DueDate = DateTime.Today },
                            },
                        },
                        new TodoListEntity
                        {
                            Name = "Work Tasks",
                            Tasks =
                            {
                                new TaskEntity { Title = "Finish report", DueDate = DateTime.Today.AddDays(1) },
                                new TaskEntity { Title = "Schedule meeting with team", DueDate = DateTime.Today.AddDays(2) },
                                new TaskEntity { Title = "Follow up with customer", DueDate = DateTime.Today.AddDays(3) },
                            },
                        },
                        new TodoListEntity
                        {
                            Name = "Personal Goals",
                            Description = "Things I want to accomplish this year",
                            Tasks =
                            {
                                new TaskEntity { Title = "Run a marathon", DueDate = DateTime.Today.AddMonths(6) },
                                new TaskEntity { Title = "Learn a new language", DueDate = DateTime.Today.AddMonths(12) },
                                new TaskEntity { Title = "Read 50 books", DueDate = DateTime.Today.AddYears(1) },
                            },
                        },
                    };

                    context.TodoLists.AddRange(todoLists);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Consider rethrowing or handling the exception appropriately.
            }
        }
    }
}
