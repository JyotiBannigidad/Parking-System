namespace CAVU.ParkingAPI.Dto
{
    public class ReservationResponseDto
    {
        public int ResponseId { get; set; }

        public string? VehicleNo { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
