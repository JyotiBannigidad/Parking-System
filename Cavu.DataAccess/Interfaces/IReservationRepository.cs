using Cavu.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.DataAccess.Interfaces
{
    public interface IReservationRepository:IGenericRepository<Reservations>
    {
        Task<Reservations> GetByReservationID(string reservationId);

        Task<Reservations> GetByReservationId(int reservationId);
    }
}
