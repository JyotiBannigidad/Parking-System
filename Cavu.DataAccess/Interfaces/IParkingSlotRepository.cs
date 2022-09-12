using Cavu.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.DataAccess.Interfaces
{
    public interface IParkingSlotRepository:IGenericRepository<ParkingSlots>
    {
        public Task<List<int>> GetAvailableSlots(DateTime from, DateTime to);
    }
}
