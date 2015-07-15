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
    public class TimeSpanEventService : ITimeSpanEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeSpanEventRepository _timeSpanEventRepository;

        public TimeSpanEventService(IUnitOfWork unitOfWork, ITimeSpanEventRepository timeSpanEventRepository)
        {
            _unitOfWork = unitOfWork;
            _timeSpanEventRepository = timeSpanEventRepository;
        }

        public Task<IEnumerable<TimeSpanEvent>> GetAllAsync()
        {
            return _timeSpanEventRepository.GetAllAsync();
        }

        public async Task<TimeSpanEvent> CreateAsync(TimeSpanEvent timeSpanEvent)
        { 
            _timeSpanEventRepository.Insert(timeSpanEvent);
            await _unitOfWork.SaveChangesAsync();
            return timeSpanEvent;
        }

        public async Task<TimeSpanEvent> GetByIdAsync(int id)
        {
            return await _timeSpanEventRepository.GetByIdAsync(id);
        }

        public async Task<TimeSpanEvent> UpdateAsync(TimeSpanEvent timeSpanEvent)
        {
            _timeSpanEventRepository.Update(timeSpanEvent);
            await _unitOfWork.SaveChangesAsync();
            return timeSpanEvent;
        }

        public void  Delete(TimeSpanEvent timeSpanEvent)
        {
            _timeSpanEventRepository.Delete(timeSpanEvent);
             _unitOfWork.SaveChanges();
        }
    }
}
