using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.Services.DTO
{
    public class ReservationResponseDto
    {
        public string ReferenceNo { get; set; }
        public string? VehicleNo { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
        public decimal? AmountPaid { get; set; }
    }
}
