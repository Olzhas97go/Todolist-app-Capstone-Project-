﻿using TodoListApp.WebApi.Entities;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Services;

using AutoMapper;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Tasks;
using TodoListApp.WebApp.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoListApp.WebApi.Models.Tasks.TodoTask, TodoListApp.WebApp.Models.Task>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.CompletedAt, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.IsCompleted));

        // Map TodoListDto to TodoListWebApiModel
        CreateMap<TodoListDetailsDto, TodoListWebApiModel>()
            .ForMember(dest => dest.Tasks, opt => opt.Ignore()) // We will handle this mapping manually
            .ReverseMap();

        // In your MappingProfile class
        CreateMap<TodoTask, TodoTaskDto>()
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());


        CreateMap<TodoListWebApiModel, TodoListDto>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore());

        CreateMap<TodoListDto, TodoListModel>();
        CreateMap<TaskEntity, TodoTask>()
            .ForMember(dest => dest.IsOverdue, opt => opt.MapFrom(src => src.DueDate < DateTime.Now))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate));

        CreateMap<TodoListDetailsDto, TodoListDto>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore()) // Or map if exists
            .ForMember(dest => dest.Status, opt => opt.Ignore()) // Or map if exists
            .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.TaskIds.Select(taskId => new TodoTaskDto
            {
                // Correctly map properties from the task fetched by ID
                // (You'll need a service to get the task details by ID)
            })));
        CreateMap<TodoTaskDto, TodoTask>()
            .ForMember(dest => dest.TodoListId, opt => opt.MapFrom(src => src.TodoListId))
            .ForMember(dest => dest.IsOverdue, opt => opt.MapFrom(src => src.IsOverdue))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

        CreateMap<TodoListDto, TodoListWebApiModel>();
    }
}
