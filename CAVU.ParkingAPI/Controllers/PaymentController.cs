using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAVU.ParkingAPI.Controllers
{
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        public ActionResult Pay()
        {
            return null;
        }

        [HttpGet]
        public Task<decimal> CalculateAmount(int reservationId)
        {
            return null;
        }

       
    }
}
