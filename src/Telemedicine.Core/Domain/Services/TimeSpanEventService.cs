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

        public async Task<DoctorTimeWindows> GetDoctorTimeWindowsAsync(int id)
        {
            var startSearchDate = DateTime.Now;
            var stopSearchDate = DateTime.Now.AddMonths(3);
            var windowsSize = 15;

            var timeWindows = new DoctorTimeWindows();
            timeWindows.WindowSize = windowsSize;
            timeWindows.MinDate = startSearchDate;
            timeWindows.MaxDate = stopSearchDate;
            timeWindows.NearestAvailable = ShiftTime(startSearchDate, windowsSize);

            return timeWindows;
        }

        private static DateTime ShiftTime(DateTime time, int windowSize)
        {
            var timeshift = time.Minute % windowSize;

            if (timeshift > 0)
            {
                var shift = windowSize - timeshift;
                time = time.AddMinutes(shift);
            }

            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
        }
    }
}
