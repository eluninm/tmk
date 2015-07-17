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
    }

    public class ConversationService: IConversationService
    {
        private readonly DataContext _dbContext;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IPaymentHistoryService _paymentService;

        public ConversationService(
            IDbContextProvider dbContextProvider, 
            SiteUserManager userManager, 
            IDoctorService doctorService, 
            IPatientService patientService, 
            IPaymentHistoryService paymentService)
        {
            _dbContext = dbContextProvider.Context;
            _userManager = userManager;
            _doctorService = doctorService;
            _patientService = patientService;
            _paymentService = paymentService;
        }

        public async Task<Conversation> BeginConversation(string creatorId, params string[] memberIds)
        {
            var creator = await _dbContext.Set<SiteUser>().FirstOrDefaultAsync(t => t.Id == creatorId);
            var members = await _dbContext.Set<SiteUser>().Where(t => memberIds.Contains(t.Id)).ToListAsync();

            var conversation = new Conversation
            {
                Created = DateTime.Now,
                Creator = creator,
                Members = members,
                Id = Guid.NewGuid().ToString()
            };

            conversation.Members.Add(creator);

            _dbContext.Set<Conversation>().Add(conversation);
            _dbContext.SaveChanges();

            var patient = await _patientService.GetByUserIdAsync(creatorId);

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
                .ToListAsync();
            return messages;
        }
    }
}
