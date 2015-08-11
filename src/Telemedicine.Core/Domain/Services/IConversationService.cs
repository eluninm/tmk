using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Data;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Identity;
using Telemedicine.Core.Models;
using Telemedicine.Core.Models.Enums;

namespace Telemedicine.Core.Domain.Services
{
    /// <summary>
    /// Interface that exposes basic text message management.
    /// </summary>
    public interface IConversationService
    {
        Task<Conversation> BeginConversation(string creatorId, params string[] memberIds);

        Task<Conversation> OpenConversation(string conversationId);

        Task<ChatMessage> SendMessage(string conversationId, string senderId, string message);

        Task<IEnumerable<Conversation>> GetUserConversations(string userId);

        Task<IEnumerable<ChatMessage>> GetConversationMessages(string conversationId);

        Task<Conversation> BeginConversation2(int patientId, int doctorId);
    }

    public class ConversationService: IConversationService
    {
        private readonly DataContext _dbContext;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IPaymentHistoryService _paymentService;
        private readonly IAppointmentEventService _appointmentEventService;
        private readonly IDoctorTimetableService _doctorTimetableService;

        public ConversationService(
            IDbContextProvider dbContextProvider, 
            SiteUserManager userManager, 
            IDoctorService doctorService, 
            IPatientService patientService, 
            IPaymentHistoryService paymentService, IAppointmentEventService appointmentEventService, IDoctorTimetableService doctorTimetableService)
        {
            _dbContext = dbContextProvider.Context;
            _userManager = userManager;
            _doctorService = doctorService;
            _patientService = patientService;
            _paymentService = paymentService;
            _appointmentEventService = appointmentEventService;
            _doctorTimetableService = doctorTimetableService;
        }

        public async Task<Conversation> BeginConversation(string creatorId, params string[] memberIds)
        {
            var creator = await _dbContext.Set<SiteUser>().FirstOrDefaultAsync(t => t.Id == creatorId);
            var members = await _dbContext.Set<SiteUser>().Where(t => memberIds.Contains(t.Id)).ToListAsync();

            var patient = await _patientService.GetByUserIdAsync(creatorId);
            var doctor = await _doctorService.GetByUserIdAsync(memberIds[0]);

            var conversation = new Conversation
            {
                Doctor = doctor,
                Patient = patient,
                Created = DateTime.Now,
                Creator = creator,
                Members = members,
                Id = Guid.NewGuid().ToString(),
                CreatorId = creator.Id
            };

            conversation.Members.Add(creator);

            _dbContext.Set<Conversation>().Add(conversation);
            _dbContext.SaveChanges();



            AppointmentEvent appointmentEvent = new AppointmentEvent()
            {
                Date = DateTime.Now,
                Doctor = await _doctorService.GetByIdAsync(doctor.Id),
                Patient = patient,
                Created = DateTime.Now,
                Status = AppointmentStatus.Closed
            };

            DoctorTimetable timetable =
                       await _doctorTimetableService.GetTimetableByDate(doctor.Id, DateTime.Now);
            if (timetable == null)
            {
                timetable = new DoctorTimetable()
                {
                    DateTime = DateTime.Now,
                    HourType = DoctorTimetableHourType.Working,
                    DoctorId = doctor.Id,
                    AppointmentEvents = new List<AppointmentEvent>()

                };
                timetable = await _doctorTimetableService.CreateAsync(timetable);
                timetable.AppointmentEvents.Add(appointmentEvent);
                timetable = await _doctorTimetableService.UpdateAsync(timetable); 
            }
            else
            {
                if (timetable.HourType != DoctorTimetableHourType.NotWorking)
                {
                    timetable.HourType = DoctorTimetableHourType.Working;
                    timetable.AppointmentEvents.Add(appointmentEvent);
                    timetable = await _doctorTimetableService.UpdateAsync(timetable); 
                }
            }

            await _paymentService.CreateAsync(new PaymentHistory
            {
                ConversationId = conversation.Id,
                Date = conversation.Created,
                Patient = patient,
                PaymentType = PaymentType.Consultation,
                Value = -500
            });


            return conversation;
        }

        public async Task<Conversation> OpenConversation(string conversationId)
        {
            return await _dbContext.Set<Conversation>()
                .Include(t => t.Messages)
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t => t.Id == conversationId);
        }

        public async Task<ChatMessage> SendMessage(string conversationId, string senderId, string message)
        {
            var conversation = await _dbContext.Set<Conversation>().FirstOrDefaultAsync(t => t.Id == conversationId);
            var sender = await _dbContext.Set<SiteUser>().FirstOrDefaultAsync(t => t.Id == senderId);

            var chatMessage = new ChatMessage
            {
                Conversation = conversation,
                Creator = sender,
                Created = DateTime.Now,
                Message = message
            };

            _dbContext.Set<ChatMessage>().Add(chatMessage);
            await _dbContext.SaveChangesAsync();
            return chatMessage;
        }

        public async Task<IEnumerable<Conversation>> GetUserConversations(string userId)
        {
            var consultations = from c in _dbContext.Set<Conversation>()
                from m in c.Members
                where m.Id == userId
                select c;
            return await consultations
                .Include(t => t.Doctor)
                .Include(t => t.Doctor.User)
                .Include(t => t.Doctor.Specialization)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetConversationMessages(string conversationId)
        {
            var messages = await _dbContext.Set<ChatMessage>()
                .Include(t => t.Creator)
                .Where(t => t.ConversationId == conversationId)
                .ToListAsync();
            return messages;
        }

        public async Task<Conversation> BeginConversation2(int patientId, int doctorId)
        {
            var patient = await _patientService.GetByIdAsync(patientId);
            var doctor = await _doctorService.GetByIdAsync(doctorId);

            var conversation = new Conversation
            {
                Doctor = doctor,
                Patient = patient,
                Created = DateTime.Now,
                Creator = patient.User,
                Id = Guid.NewGuid().ToString()
            };

            _dbContext.Set<Conversation>().Add(conversation);
            await _dbContext.SaveChangesAsync();

            //await _paymentService.CreateAsync(new PaymentHistory
            //{
            //    ConversationId = conversation.Id,
            //    Date = conversation.Created,
            //    Patient = patient,
            //    PaymentType = PaymentType.Consultation,
            //    Value = -500
            //});

            return conversation;
        }
    }
}
