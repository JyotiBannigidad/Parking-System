using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.Services.DTO
{
    public class EditReservationDto
    {
      
            public int ReservationId { get; set; }
            public string? VehicleNo { get; set; }
            public string? VehicleType { get; set; }

            public string? Category { get; set; }

            public DateTime FromDate { get; set; }

            public DateTime ToDate { get; set; }

            public decimal Amount { get; set; }
        
    }
}
