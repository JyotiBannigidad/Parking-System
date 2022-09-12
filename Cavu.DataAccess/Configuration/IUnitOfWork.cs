using Cavu.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.DataAccess.Configuration
{
    public interface IUnitOfWork
    {
       IReservationRepository  Reservations { get; }
        IParkingSlotRepository ParkingSlots { get; }
        Task CompleteAsync();
        void Commit();
        
    }
}
