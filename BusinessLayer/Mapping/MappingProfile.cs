﻿using AutoMapper;
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
            CreateMap<CustomerOperation, CompleteOperationRequest>().ReverseMap();
            CreateMap<CustomerOperation, CancelOperationRequest>().ReverseMap();
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

        }
    }
}
