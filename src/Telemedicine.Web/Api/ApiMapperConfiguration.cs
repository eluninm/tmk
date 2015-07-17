using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Telemedicine.Core.Models;
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Api
{
    public static class ApiMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Recommendation, RecommendationDto>()
                .ForMember(d => d.Id, e => e.MapFrom(s => s.Id))
                .ForMember(d => d.Created, e => e.MapFrom(s => s.CreateDate))
                .ForMember(d => d.DoctorSpecialization, e => e.MapFrom(s => s.Doctor.Specialization.DisplayName))
                .ForMember(d => d.DoctorTitle, e => e.MapFrom(s => s.Doctor.User.DisplayName));

            Mapper.CreateMap<Conversation, ConsultationDto>()
                .ForMember(d => d.Id, e => e.MapFrom(s => s.Id))
                .ForMember(d => d.DoctorTitle, e => e.MapFrom(s => s.Doctor.User.DisplayName))
                .ForMember(d => d.DoctorSpecialization, e => e.MapFrom(s => s.Doctor.Specialization.DisplayName));

            Mapper.CreateMap<ChatMessage, ConsultationMessageDto>()
                .ForMember(d => d.UserName, e => e.MapFrom(s => s.Creator.UserName));
        }
    }
}