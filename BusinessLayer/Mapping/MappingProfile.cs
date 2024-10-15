using AutoMapper;
using DtoLayer.Customer.Requests;
using DtoLayer.Customer.Responses;
using DtoLayer.CustomerOperation.Requests;
using DtoLayer.CustomerOperation.Responses;
using DtoLayer.TaskAssignment.Requests;
using DtoLayer.TaskAssignment.Responses;
using DtoLayer.TaskAssignmentFile.Requests;
using DtoLayer.User.Requests;
using DtoLayer.User.Responses;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, AddCustomerRequest>().ReverseMap();
            CreateMap<Customer, UpdateCustomerRequest>().ReverseMap();
            CreateMap<Customer, DisplayCustomerResponse>().ReverseMap();
            CreateMap<Customer, GetByIdCustomerResponse>().ReverseMap();
            CreateMap<Customer, CustomerSearchResponse>().ReverseMap();
            CreateMap<Customer, GetProfileInfoByIdResponse>().ReverseMap();

            CreateMap<CustomerOperation, AddCustomerOperationRequest>().ReverseMap();
            CreateMap<CustomerOperation, FilterCustomerOperationRequest>().ReverseMap();
            CreateMap<CustomerOperation, CompleteOperationRequest>().ReverseMap();
            CreateMap<CustomerOperation, CancelOperationRequest>().ReverseMap();


            CreateMap<TaskAssignment, AddTaskAssignmentRequest>().ReverseMap();
            CreateMap<TaskAssignment, TaskAssignmentCountResponse>().ReverseMap();
            CreateMap<TaskAssignment, UpdateTaskAssignmentStatusToOfferGivenRequest>().ReverseMap();
            CreateMap<TaskAssignment, UpdateTaskStatusToProposalGivenRequest>().ReverseMap();
            CreateMap<TaskAssignment, UpdateTaskStatusToApprovedRequest>().ReverseMap();
            CreateMap<TaskAssignment, UpdateTaskStatusToRejectedRequest>().ReverseMap();
            CreateMap<TaskAssignment, UpdateTaskAssignmentRequest>().ReverseMap();
            CreateMap<TaskAssignment, DisplayTaskAssignmentResponse>().ReverseMap();
            CreateMap<TaskAssignment, GetByIdTaskAssignmentResponse>()
             .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CustomerOperation.User.FullName))
             .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerOperation.Customer.CompanyName)).ReverseMap();


            CreateMap<TaskAssignmentEfDto, GetPendingTaskAssignmentResponse>()
             .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser))
             .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
             .ForMember(dest => dest.AbasId, opt => opt.MapFrom(src => src.AbasId))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
             .ForMember(dest => dest.Quantity1, opt => opt.MapFrom(src => src.Quantity1))
             .ForMember(dest => dest.Quantity2, opt => opt.MapFrom(src => src.Quantity2))
             .ForMember(dest => dest.Quantity3, opt => opt.MapFrom(src => src.Quantity3))
             .ForMember(dest => dest.Quantity4, opt => opt.MapFrom(src => src.Quantity4))
             .ForMember(dest => dest.Quantity5, opt => opt.MapFrom(src => src.Quantity5))
             .ForMember(dest => dest.Quantity6, opt => opt.MapFrom(src => src.Quantity6))
             .ForMember(dest => dest.Quantity7, opt => opt.MapFrom(src => src.Quantity7))
             .ForMember(dest => dest.Quantity8, opt => opt.MapFrom(src => src.Quantity8))
             .ForMember(dest => dest.Quantity9, opt => opt.MapFrom(src => src.Quantity9))
             .ForMember(dest => dest.Quantity10, opt => opt.MapFrom(src => src.Quantity10));


            CreateMap<TaskAssignmentEfDto, GetComplatedTaskAssignmentResponse>()
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.AbasId, opt => opt.MapFrom(src => src.AbasId))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CompletedDate, opt => opt.MapFrom(src => src.CompletedDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Quantity1, opt => opt.MapFrom(src => src.Quantity1))
            .ForMember(dest => dest.Quantity2, opt => opt.MapFrom(src => src.Quantity2))
            .ForMember(dest => dest.Quantity3, opt => opt.MapFrom(src => src.Quantity3))
            .ForMember(dest => dest.Quantity4, opt => opt.MapFrom(src => src.Quantity4))
            .ForMember(dest => dest.Quantity5, opt => opt.MapFrom(src => src.Quantity5))
            .ForMember(dest => dest.Quantity6, opt => opt.MapFrom(src => src.Quantity6))
            .ForMember(dest => dest.Quantity7, opt => opt.MapFrom(src => src.Quantity7))
            .ForMember(dest => dest.Quantity8, opt => opt.MapFrom(src => src.Quantity8))
            .ForMember(dest => dest.Quantity9, opt => opt.MapFrom(src => src.Quantity9))
            .ForMember(dest => dest.Quantity10, opt => opt.MapFrom(src => src.Quantity10));
            

            CreateMap<TaskAssignmentEfDto, GetProposalGivenTaskAssignmentResponse>()
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.AbasId, opt => opt.MapFrom(src => src.AbasId))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Quantity1, opt => opt.MapFrom(src => src.Quantity1))
            .ForMember(dest => dest.Quantity2, opt => opt.MapFrom(src => src.Quantity2))
            .ForMember(dest => dest.Quantity3, opt => opt.MapFrom(src => src.Quantity3))
            .ForMember(dest => dest.Quantity4, opt => opt.MapFrom(src => src.Quantity4))
            .ForMember(dest => dest.Quantity5, opt => opt.MapFrom(src => src.Quantity5))
            .ForMember(dest => dest.Quantity6, opt => opt.MapFrom(src => src.Quantity6))
            .ForMember(dest => dest.Quantity7, opt => opt.MapFrom(src => src.Quantity7))
            .ForMember(dest => dest.Quantity8, opt => opt.MapFrom(src => src.Quantity8))
            .ForMember(dest => dest.Quantity9, opt => opt.MapFrom(src => src.Quantity9))
            .ForMember(dest => dest.Quantity10, opt => opt.MapFrom(src => src.Quantity10));

            CreateMap<TaskAssignment, TaskAssignmentEfDto>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CustomerOperation.User.FullName))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerOperation.Customer.CompanyName))
                .ForMember(dest => dest.AbasId, opt => opt.MapFrom(src => src.AbasId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.CompletedDate, opt => opt.MapFrom(src => src.CompletedDate))
                .ForMember(dest => dest.Quantity1, opt => opt.MapFrom(src => src.Quantity1))
                .ForMember(dest => dest.Quantity2, opt => opt.MapFrom(src => src.Quantity2))
                .ForMember(dest => dest.Quantity3, opt => opt.MapFrom(src => src.Quantity3))
                .ForMember(dest => dest.Quantity4, opt => opt.MapFrom(src => src.Quantity4))
                .ForMember(dest => dest.Quantity5, opt => opt.MapFrom(src => src.Quantity5))
                .ForMember(dest => dest.Quantity6, opt => opt.MapFrom(src => src.Quantity6))
                .ForMember(dest => dest.Quantity7, opt => opt.MapFrom(src => src.Quantity7))
                .ForMember(dest => dest.Quantity8, opt => opt.MapFrom(src => src.Quantity8))
                .ForMember(dest => dest.Quantity9, opt => opt.MapFrom(src => src.Quantity9))
                .ForMember(dest => dest.Quantity10, opt => opt.MapFrom(src => src.Quantity10));
            

            CreateMap<CustomerOperation, DisplayCustomerOperationsStatusGivenOffersResponse>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : "Unknown"))
             .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CompanyName : "Unknown")).ReverseMap();

            CreateMap<CustomerOperation, UpdateCustomerOperationRequest>().ReverseMap();
            CreateMap<CustomerOperation, DisplayCustomerOperationResponse>()
             .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : "Unknown"))
             .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CompanyName : "Unknown"))
              .ForMember(dest => dest.OperationDate, opt => opt.MapFrom(src => src.OperationDate));
            CreateMap<CustomerOperation, GetByIdCustomerOperationResponse>().ReverseMap();
            CreateMap<CustomerOperation, DisplayCustomerOperationByCustomerResponse>()
             .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : "Unknown"))
              .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CompanyName : "Unknown"));

            CreateMap<CustomerOperation, DisplayEmailInteractionCountResponse>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Customer.CompanyName))
             .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForMember(dest => dest.InteractionCount, opt => opt.Ignore());



            CreateMap<CustomerOperation, DisplayPhoneInteractionCountResponse>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Customer.CompanyName))
             .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForMember(dest => dest.InteractionCount, opt => opt.Ignore());

            CreateMap<CustomerOperation, DisplayFaceToFaceInteractionCountResponse>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Customer.CompanyName))
             .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForMember(dest => dest.InteractionCount, opt => opt.Ignore());

            CreateMap<CustomerOperation, DisplayUserEmailInteractionCountResponse>()
           .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Customer.CompanyName))
           .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
           .ForMember(dest => dest.InteractionCount, opt => opt.Ignore());

            CreateMap<CustomerOperation, DisplayUserPhoneInteractionCountResponse>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Customer.CompanyName))
             .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForMember(dest => dest.InteractionCount, opt => opt.Ignore());

            CreateMap<CustomerOperation, DisplayUserFaceToFaceInteractionCountResponse>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Customer.CompanyName))
             .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForMember(dest => dest.InteractionCount, opt => opt.Ignore());


            CreateMap<User, AddUserRequest>().ReverseMap()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<User, UpdateUserRequest>().ReverseMap();
            CreateMap<User, DisplayUserResponse>().ReverseMap();
            CreateMap<User, GetByIdUserResponse>().ReverseMap();

            CreateMap<AddCustomerOperationWithDisplayRequest, AddCustomerOperationRequest>()
           .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.OperationRequest.CustomerId))
           .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.OperationRequest.Method))
           .ForMember(dest => dest.IsNew, opt => opt.MapFrom(src => src.OperationRequest.IsNew))
           .ForMember(dest => dest.PlannedDate, opt => opt.MapFrom(src => src.OperationRequest.PlannedDate))
           .ForMember(dest => dest.ActualDate, opt => opt.MapFrom(src => src.OperationRequest.ActualDate))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.OperationRequest.Description))
           .ForMember(dest => dest.ContactPerson, opt => opt.MapFrom(src => src.OperationRequest.ContactPerson))
           .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.OperationRequest.Reason))
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.OperationRequest.Address))
           .ReverseMap();

            //CreateMap<DisplayCustomerResponse, AddCustomerOperationWithDisplayRequest>()
            //    .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src))
            //    .ReverseMap();

            CreateMap<AddTaskAssignmentFileRequest, TaskAssignmentFile>()
            .ForMember(dest => dest.FileName, opt => opt.Ignore())  
            .ForMember(dest => dest.FilePath, opt => opt.Ignore())  
            .ForMember(dest => dest.UploadedDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<TaskAssignment, TaskAssignmentFile>() 
            .ForMember(dest => dest.TaskAssignmentId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
