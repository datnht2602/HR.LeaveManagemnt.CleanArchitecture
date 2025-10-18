using AutoMapper;
using HR.LeaveManagement.BlazorUI.Models;
using HR.LeaveManagement.BlazorUI.Models.LeaveAllocations;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
        CreateMap<LeaveTypeDetailsDto, LeaveTypeVM>().ReverseMap();
        CreateMap<LeaveTypeVM, CreateLeaveTypeCommand>().ReverseMap();
        CreateMap<LeaveTypeVM, UpdateLeaveTypeCommand>().ReverseMap();
        
        CreateMap<LeaveRequestsDto, LeaveRequestVM>()
            .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
            .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
            .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
            .ReverseMap();
        CreateMap<LeaveRequestDetailsDto, LeaveRequestVM>()
            .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
            .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
            .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
            .ReverseMap();
        CreateMap<CreateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();
        CreateMap<UpdateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();
        
        CreateMap<LeaveAllocationDto, LeaveAllocationVM>().ReverseMap();
        CreateMap<LeaveAllocationDetailsDto, LeaveAllocationVM>().ReverseMap();
        CreateMap<CreateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();
        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();
        
        CreateMap<EmployeeVM, Employee>().ReverseMap();
    }
}