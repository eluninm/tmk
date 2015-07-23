using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;
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

            Mapper.CreateMap<Doctor, DoctorListItemDto>()
                .ForMember(t => t.Title, e => e.MapFrom(s => s.User.DisplayName))
                .ForMember(t => t.Specialization, e => e.MapFrom(s => s.Specialization.DisplayName))
                .ForMember(t => t.AvatarUrl, e => e.MapFrom(s => s.User.AvatarUrl))
                .ForMember(t => t.StatusText, e => e.MapFrom(s => s.DoctorStatus.DisplayName))
                .ForMember(t => t.StatusName, e => e.MapFrom(s => s.DoctorStatus.Name));

            Mapper.CreateMap<Specialization, SpecializationDto>();

            Mapper.CreateMap<AppointmentEvent, DoctorAppointmentDto>()
                .ForMember(d => d.PatientTitle, e => e.MapFrom(s => s.Patient.User.DisplayName))
                .ForMember(d => d.PatientAvatarUrl, e => e.MapFrom(s => s.Patient.User.AvatarUrl));

            Mapper.CreateMap<AppointmentEvent, PatientAppointmentDto>()
                .ForMember(d => d.DoctorTitle, e => e.MapFrom(s => s.Doctor.User.DisplayName))
                .ForMember(d => d.DoctorSpecialization, e => e.MapFrom(s => s.Doctor.Specialization.DisplayName));

            Mapper.CreateMap<PaymentHistory, PaymentHistoryDto>()
                .ForMember(d => d.Date, e => e.MapFrom(s => s.Date))
                .ForMember(d => d.Value, e => e.MapFrom(s => s.Value))
                .ForMember(d => d.PaymentType, e => e.MapFrom(s => s.PaymentType == PaymentType.Replenishment? "Пополнение": "Консультация"));
        }
    }
}