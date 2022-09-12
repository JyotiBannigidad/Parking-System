using Cavu.DataAccess.Interfaces;
using Cavu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.DataAccess.Configuration
{
    public class UnitOfWork:IUnitOfWork , IDisposable
    {
        private readonly ApplicationDbContext _context;

        public IReservationRepository Reservations { get; private set; }

        public IParkingSlotRepository ParkingSlots { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Reservations = new ReservationRepository(context);
            ParkingSlots = new ParkingSlotRepository(context);

        }
        public void Commit()
        {
            try
            {
                 _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }


        public async Task CompleteAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }         

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
