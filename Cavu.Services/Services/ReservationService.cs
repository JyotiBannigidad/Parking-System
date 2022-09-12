using AutoMapper;
using Cavu.DataAccess.Configuration;
using Cavu.DataAccess.Entities;
using Cavu.Services.DTO;
using Cavu.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.Services.Services
{
    public class ReservationService:IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IParkingSlotService _parkingSlotService;

        public ReservationService(IMapper mapper,IUnitOfWork unitOfWork, IParkingSlotService parkingSlotService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _parkingSlotService = parkingSlotService;

        }

        public async Task<ReservationResponseDto> Reserve(CreateReservationDto createReservationDto)
        {
            ReservationResponseDto responseNew = new ReservationResponseDto();
            try
            {                
                int slotId = 0;
                var listSlots = _unitOfWork.ParkingSlots.Entities.Where(x => x.Status == "Available");
                
                if(listSlots != null )
                {
                     slotId = listSlots.Select(x => x.Id).FirstOrDefault();
                }

                createReservationDto.Amount = await CalculateAmount(createReservationDto.FromDate, createReservationDto.ToDate);

                Reservations reservations = _mapper.Map<Reservations>(createReservationDto);
                reservations.ParkingSlotsId = slotId;
                reservations.ReferenceNo = getUniqueRef();
                var response = await _unitOfWork.Reservations.Add(reservations);

                var parkingSlot = await _unitOfWork.ParkingSlots.GetById(slotId);
                if(parkingSlot != null)
                {
                    parkingSlot.Status = "Reserved";
                }
                await _unitOfWork.CompleteAsync();
                
                if (reservations.Id != 0)
                {
                    responseNew.VehicleNo = reservations.VehicleNo;
                    responseNew.ReferenceNo = reservations.ReferenceNo;
                    responseNew.FromDate = reservations.FromDate;
                    responseNew.ToDate = reservations.ToDate;
                    responseNew.AmountPaid = reservations.Amount;
                }
                return responseNew;
            }
            catch (Exception ex)
            {
                
            }
            return responseNew;
        }

        public async Task<ReservationResponseDto> EditReservation(EditReservationDto editReservationDto)
        {
            if (editReservationDto != null)
            {
                var reservationDetails =  _unitOfWork.Reservations.Entities.Where(r => r.Id == editReservationDto.ReservationId).FirstOrDefault();
                //_unitOfWork.Reservations.GetById(editReservationDto.ReservationId);
                ReservationResponseDto responseNew = new ReservationResponseDto();
                try
                {
                    int slotId = 0;
                    //var listSlots = _unitOfWork.ParkingSlots.Entities.Where(x => x.Status == "Available");

                    //if (listSlots != null)
                    //{
                    //    slotId = listSlots.Select(x => x.Id).FirstOrDefault();
                    //}

                    var Amount = await CalculateAmount(editReservationDto.FromDate, editReservationDto.ToDate);
                    decimal balance = 0.0m;
                    if (editReservationDto.Amount > Amount)
                    {
                        balance = editReservationDto.Amount - Amount;
                        bool status = await CreateRefundRequest(reservationDetails.Id ,balance);
                        editReservationDto.Amount = Amount;
                    }
                    else
                    {
                        editReservationDto.Amount = Amount;
                        balance = Amount - editReservationDto.Amount;
                        var s =  MakeExtraPayment(reservationDetails.Id,  balance);
                    }


                    editReservationDto.Amount = await CalculateAmount(editReservationDto.FromDate, editReservationDto.ToDate);

                    Reservations reservations = _mapper.Map<Reservations>(editReservationDto);
                    reservations.ParkingSlotsId = slotId;
                    reservations.ReferenceNo = getUniqueRef();
                    reservations.FromDate = editReservationDto.FromDate;
                    reservations.ToDate = editReservationDto.ToDate;
                    reservationDetails.Amount = editReservationDto.Amount;


                    //var parkingSlot = await _unitOfWork.ParkingSlots.GetById(slotId);
                    //if (parkingSlot != null)
                    //{
                    //    parkingSlot.Status = "Reserved";
                    //}
                    await _unitOfWork.CompleteAsync();

                    if (reservationDetails.Id != 0)
                    {
                        responseNew.VehicleNo = reservations.VehicleNo;
                        responseNew.ReferenceNo = reservations.ReferenceNo;
                        responseNew.FromDate = reservations.FromDate;
                        responseNew.ToDate = reservations.ToDate;
                        responseNew.AmountPaid = reservations.Amount;
                       
                    }
                    return responseNew;
                }
                catch (Exception ex)
                {

                }
                return responseNew;
            }
            return null;
            
        }

        private Task MakeExtraPayment(int reservationId, decimal amount)
        {
            // User should pay excess amount
            return null;
        }

        public async Task<string> CancelReservation(int reservationId)
        {

            try
            {
                var reservationDetails =  _unitOfWork.Reservations.Entities.Where(r => r.Id == reservationId).FirstOrDefault();
                ReservationResponseDto responseNew = new ReservationResponseDto();
                if(reservationDetails != null)
                {

                    int slotId = reservationDetails.ParkingSlotsId;
                    
                    var refundStatus = await CreateRefundRequest(reservationId, (decimal)reservationDetails.Amount);

                    Reservations reservations = _mapper.Map<Reservations>(reservationDetails);
                    reservations.ParkingSlotsId = slotId;
                    reservations.ReferenceNo = getUniqueRef();
                    reservations.ReservationStatus = "Cancelled";


                    var parkingSlot = await _unitOfWork.ParkingSlots.GetById(slotId);
                    if (parkingSlot != null)
                    {
                        parkingSlot.Status = "Available";
                    }
                    await _unitOfWork.CompleteAsync();
                }
                

                
            }
            catch (Exception ex)
            {

            }
            return "Cancelled";
        }

        private async Task<bool> CreateRefundRequest(int reservationId, decimal amount)
        {
            //RefundLogic
            return  false;
        }


        private string getUniqueRef()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        private async Task<Dictionary<DateTime, int>> getAvailability(DateTime startDate, DateTime endDate)
        {                        
            var availableSlots= await _parkingSlotService.GetAvailableSlots(startDate, endDate);
            return availableSlots;
        }

        private async Task<Decimal> CalculateAmount(DateTime fromDate, DateTime toDate )
        {
            decimal amount = 0.0m;
            string season = "Summer"; // Should be read from the Configuration
            int noOfDays = Common.Helper.GetNoOfDays(fromDate, toDate);
            decimal amountPerDay = 0.0m;
            decimal Total = 0.0m;
            int discount = 0; // in percentage
            decimal discountAmount = 0.0m;
            decimal FinalAmount = 0.0m;
            //switch case can be used, SHould be based on the configuration
            if (season == "Summer")
            {
                amountPerDay = 8.0m;
                Total = amountPerDay * noOfDays;

                discount = 2;//(InPercentage)
                discountAmount = Total * discount / 100;
                FinalAmount = Total - discountAmount;
            }
            else if(season == "Winter")
            {
                amountPerDay = 10.0m;
                Total = amountPerDay * noOfDays;

                discount = 1;//(InPercentage) // discount can be applicable to Disabled / Senior citizens
                discountAmount = Total * discount / 100;
                FinalAmount = Total - discountAmount;
            }
            //Have a separate table to hold the price info and read from the table based on the config
            

            return FinalAmount;

        }
    }
}
