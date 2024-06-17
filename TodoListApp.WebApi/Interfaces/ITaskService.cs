﻿namespace TodoListApp.WebApi.Interfaces;

using TodoListApp.WebApi.Models.Tasks;
using TodoListApp.WebApi.Models;

public interface ITaskService
{
    Task<List<TaskModel>> GetTasksForTodoListAsync(int todoListId);  // US05

    Task<TaskModel> GetTaskByIdAsync(int taskId);                   // US06

    Task<TaskModel> AddTaskAsync(TaskEntity taskEntity); // US07

    Task<bool> DeleteTaskAsync(int taskId);                         // US08

    Task<TaskModel> UpdateTaskAsync(int taskId, TaskModel updatedTask);

    Task<TaskModel> UpdateTaskStatusAsync(int todoListId, int taskId, ToDoTaskStatus newStatus);
}
