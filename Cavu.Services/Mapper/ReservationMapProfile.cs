using AutoMapper;
using Cavu.DataAccess.Entities;
using Cavu.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.Services.Mapper
{
    public class ReservationMapProfile:Profile
    {
        public ReservationMapProfile()
        {
            CreateMap<CreateReservationDto, Reservations>();
            CreateMap<EditReservationDto, Reservations>();
        }
    }
}
