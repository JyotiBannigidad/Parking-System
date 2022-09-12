using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.Services.Interfaces
{
    public interface IParkingSlotService
    {
        // Task<List<int>> GetAvailableSlots(DateTime from, DateTime to);

        Task<Dictionary<DateTime, int>> GetAvailableSlots(DateTime startDate, DateTime endDate);
    }
}
