using Cavu.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.DataAccess.Entities
{
    public class Reservations : BaseEntity
    {
        [Required]
        public string? VehicleNo { get; set; }

        [ForeignKey("ParkingSlotsId")]
        public int ParkingSlotsId { get; set; }

        public virtual ParkingSlots? ParkingSlots { get; set; }

        public string? VehicleType { get; set; }

        public string? Category { get; set; }

        public string? ReferenceNo { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public decimal? Amount { get; set; }

        public bool Paid { get; set; } = false;

        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string? ModifiedBy { get; set; }

        public string? ReservationStatus { get; set; }

    }
}
