using Cavu.Services.Interfaces;
using Cavu.Services.Services;
using CAVU.ParkingAPI.RequestDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CAVU.ParkingAPI.Controllers
{
    public class ParkingSlotController : ControllerBase
    {
        private readonly IParkingSlotService _parkingService;

        public ParkingSlotController(IParkingSlotService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpPost]
        [Route("api/GetAvailableParkingSlots")]
        [Consumes("application/json")]
        [SwaggerOperation(Tags = new[] { "Parking Reservation" })]
        public async Task<IActionResult> GetAvailableParkingSlots(DateTime startDate, DateTime endDate)
        {
            if(!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }            
            var slots = await _parkingService.GetAvailableSlots(startDate, endDate);
            return Ok(slots);

            
        }
    }
}
