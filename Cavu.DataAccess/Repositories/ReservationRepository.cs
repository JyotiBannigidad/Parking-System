using Cavu.DataAccess.Entities;
using Cavu.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.DataAccess.Repositories
{
    public class ReservationRepository : GenericRepository<Reservations>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context):base(context)
        {

        }        

        public override Task<bool> Add(Reservations entity)
        {
            return base.Add(entity);
        }

        public Task<IEnumerable<Reservations>> All()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Reservations> GetByReservationId(int reservationId)
        {
            var detail=  await context.Reservations.Where(x => x.Id  == reservationId).FirstOrDefaultAsync();
            return detail;
        }
            

        public Task<Reservations> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Reservations> GetByReservationID(string reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Reserve(Reservations reservationDetails)
        {
            dbSet.Add(reservationDetails);
            return true;
        }

        public async Task<bool> UpdateReservationAsync(Reservations entity)
        {
            try
            {
                var reservationDet = context.Reservations.Where(x => x.Id == entity.Id).FirstOrDefault();

                if (reservationDet != null)
                {
                    reservationDet.FromDate  = entity.FromDate;
                    reservationDet.ToDate = entity.ToDate;
                    reservationDet.Amount = entity.Amount;                    
                    dbSet.Attach(reservationDet);
                    this.context.Entry(reservationDet).State = EntityState.Modified;
                    this.context.Set<Reservations>().Update(reservationDet);
                    _ = this.context.SaveChanges();
                    return true;
                }
            }            
            catch (Exception ex)
            {
                //Logging
            }
            return false;
        }

        public Task<bool> Upsert(Reservations entity)
        {
            throw new NotImplementedException();
        }
    }
}
