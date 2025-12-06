using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Task mappings
        CreateMap<Domain.Entities.Task, TaskDto>()
            .ForMember(dest => dest.TaskTypeName, opt => opt.MapFrom(src => src.TaskType.Name));
        
        CreateMap<CreateTaskDto, Domain.Entities.Task>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.TaskType, opt => opt.Ignore());
        
        CreateMap<UpdateTaskDto, Domain.Entities.Task>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.TaskType, opt => opt.Ignore());

        // TaskType mappings
        CreateMap<TaskType, TaskTypeDto>();
    }
}
