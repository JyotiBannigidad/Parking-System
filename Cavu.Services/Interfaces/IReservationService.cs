using Cavu.DataAccess.Entities;
using Cavu.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.Services.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationResponseDto> Reserve(CreateReservationDto  createReservationDto);

        Task<ReservationResponseDto> EditReservation(EditReservationDto createReservationDto);

        Task<string> CancelReservation(int reservationId);

    }
}
