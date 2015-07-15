using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Repositories;
using Telemedicine.Core.Domain.Uow;
using Telemedicine.Core.Models;

namespace Telemedicine.Core.Domain.Services
{
    public class UserEventsService : IUserEventsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserEventRepository _userEventRepository;

        public UserEventsService(IUnitOfWork unitOfWork, IUserEventRepository userEventRepository)
        {
            _unitOfWork = unitOfWork;
            _userEventRepository = userEventRepository;
        }
        
        public async Task<IEnumerable<UserEvent>> GetAllAsync()
        {
            return await _userEventRepository.GetAllAsync();            
        }

        public async Task<UserEvent> CreateAsync(UserEvent userEvent)
        {
            _userEventRepository.Insert(userEvent);
            await _unitOfWork.SaveChangesAsync();
            return userEvent;
        }

        public async Task<UserEvent> UpdateAsync(UserEvent newUserEvent)
        {
            _userEventRepository.Update(newUserEvent);
            await _unitOfWork.SaveChangesAsync();
            return newUserEvent;
        }
    }
}
