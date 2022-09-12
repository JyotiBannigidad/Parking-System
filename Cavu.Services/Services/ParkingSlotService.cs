using AutoMapper;
using Cavu.DataAccess.Configuration;
using Cavu.Services.Interfaces;
using Cavu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.Services.Services
{
    public class ParkingSlotService:IParkingSlotService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        

        public ParkingSlotService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }

        public async Task<Dictionary<DateTime ,int>> GetAvailableSlots(DateTime startDate, DateTime endDate)
        {
            int days = endDate.Subtract(startDate).Days;
            var days1 = Enumerable.Range(0, days)
                             .Select(day => startDate.AddDays(day))
                             .Count();
            

            var slots = await _unitOfWork.ParkingSlots.GetAvailableSlots(startDate, endDate);           
            DateTime[] dates = Helper.GetDatesBetween(startDate, endDate).ToArray();
            IDictionary<DateTime, int> dateSlots = new Dictionary<DateTime, int>();
            foreach (var date in dates)
            {                
                dateSlots.Add(date, slots.Count);
            }            
            
            return (Dictionary<DateTime, int>)dateSlots;
        }

       
    }
}
