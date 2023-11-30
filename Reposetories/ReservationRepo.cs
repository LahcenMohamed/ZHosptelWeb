using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZHosptel.Models;
using ZHosptel.Reposetories.DataHalper;

namespace ZHosptel.Reposetories
{
    public class ReservationRepo(HosptelWebDb hosptelWebDb) : IReservationHalper
    {
        private readonly HosptelWebDb hosptelWebDb = hosptelWebDb;

        public async Task Add(Reservation table)
        {
            hosptelWebDb.Reservations.Add(table);
            await hosptelWebDb.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var res = await GetById(id);
            hosptelWebDb.Reservations.Remove(res);
            await hosptelWebDb.SaveChangesAsync();

        }

        public async Task<List<Reservation>> GetAll()
        {
            var lst = await hosptelWebDb.Reservations.ToListAsync();
            return lst;
        }

        public async Task<Reservation> GetById(int id)
        {
            var reser = await hosptelWebDb.Reservations.FindAsync(id);
            return reser;
        }

        public async Task<List<Reservation>> reservationsPatient(int id)
        {
            var lst = await hosptelWebDb.Reservations.Include(r => r.Room).Where(r => r.RoomId == id).ToListAsync();
            return lst;
        }

        public async Task Update(Reservation table)
        {
            hosptelWebDb.Reservations.Update(table);
            await hosptelWebDb.SaveChangesAsync();
        }
    }
}
