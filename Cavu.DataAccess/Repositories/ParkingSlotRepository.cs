using Cavu.DataAccess.Entities;
using Cavu.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.DataAccess.Repositories
{
    public class ParkingSlotRepository : GenericRepository<ParkingSlots>, IParkingSlotRepository
    {
        public ParkingSlotRepository(ApplicationDbContext context):base(context)
        {

        }

        public async Task<List<int>> GetAvailableSlots(DateTime from, DateTime to)
        {
            var list = await context.ParkingSlots.Where(x =>
             //x.StartDate >= from &&
             //x.EndDate <= to &&            
             x.Status == "Available")
              .Select(x => x.Id).ToListAsync();

            //var list = await context.ParkingSlots
            //    .Select(x => x.Id).ToListAsync();

            return list;
        }
    }
}
