using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.DataAccess.Entities
{
    public class ParkingSlots :BaseEntity
    {
        public string? VehicleType { get; set; }
        

        public string? Status { get; set; }

        public virtual Reservations Reservation { get; set; }

    }
}
