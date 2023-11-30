using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZHosptel.Models;
using ZHosptel.Reposetories.DataHalper;

namespace ZHosptel.Reposetories
{
    public class RoomRepo : IRoomHalper
    {
        private readonly HosptelWebDb hosptelWebDb;

        public RoomRepo(HosptelWebDb hosptelWebDb)
        {
            this.hosptelWebDb = hosptelWebDb;
        }
        public async Task Add(Room talbe)
        {
            hosptelWebDb.Rooms.Add(talbe);
            await hosptelWebDb.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var room = await GetById(id);
            hosptelWebDb.Rooms.Remove(room);
            await hosptelWebDb.SaveChangesAsync();
        }

        public async Task<List<Room>> GetAll()
        {
            var lst = await hosptelWebDb.Rooms.ToListAsync();
            return lst;
        }

        public async Task<Room> GetById(int id)
        {
            var room = await hosptelWebDb.Rooms.FindAsync(id);
            return room;
        }

        public async Task Update(Room talbe)
        {
            hosptelWebDb.Rooms.Update(talbe);
            await hosptelWebDb.SaveChangesAsync();
        }
    }
}
