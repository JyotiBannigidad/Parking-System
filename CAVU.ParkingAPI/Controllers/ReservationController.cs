using Cavu.Services.DTO;
using Cavu.Services.Interfaces;
using Cavu.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace CAVU.ParkingAPI.Controllers
{
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        [Route("api/Reserve")]
        [Consumes("application/json")]
        [SwaggerOperation(Tags = new[] { "Parking Reservation" })]
        public async Task<IActionResult> Reserve([FromBody] CreateReservationDto createReservationDto)
        {

            if(!ModelState.IsValid)
            {                
                return BadRequest(ModelState) ;
            }
            var reservationStatus = await _reservationService.Reserve(createReservationDto);
            return Ok(reservationStatus);
        }

        [HttpPut]
        [Route("api/EditReservation")]
        [Consumes("application/json")]
        [SwaggerOperation(Tags = new[] { "Parking Reservation" })]
        public async Task<IActionResult> EditReservation([FromBody] EditReservationDto editReservationDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservationStatus = await _reservationService.EditReservation(editReservationDto);
            return Ok(reservationStatus);
        }

        [HttpDelete]
        [Route("api/CancelReservation")]
        [SwaggerOperation(Tags = new[] { "Parking Reservation" })]
        public async Task<IActionResult> CancelReservation(int reservationId)
        {
            var reservationStatus = await _reservationService.CancelReservation(reservationId);
            return Ok(reservationStatus);
        }


    }
}
