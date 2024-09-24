using AutoMapper;
using DtoLayer.Customer.Requests;
using DtoLayer.Customer.Responses;
using DtoLayer.CustomerOperation.Requests;
using DtoLayer.CustomerOperation.Responses;
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

            CreateMap<CustomerOperation, AddCustomerOperationRequest>().ReverseMap();
            CreateMap<CustomerOperation, UpdateCustomerOperationRequest>().ReverseMap();
            CreateMap<CustomerOperation, DisplayCustomerOperationResponse>()
             .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : "Unknown")) 
             .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CompanyName : "Unknown"))
              .ForMember(dest => dest.OperationDate, opt => opt.MapFrom(src => src.OperationDate));
            CreateMap<CustomerOperation, GetByIdCustomerOperationResponse>().ReverseMap();
            CreateMap<CustomerOperation, DisplayCustomerOperationByCustomerResponse>()
             .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : "Unknown"))
              .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CompanyName : "Unknown"));

            CreateMap<User, AddUserRequest>().ReverseMap()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<User, UpdateUserRequest>().ReverseMap();
            CreateMap<User, DisplayUserResponse>().ReverseMap();
            CreateMap<User, GetByIdUserResponse>().ReverseMap();



        }
    }
}
