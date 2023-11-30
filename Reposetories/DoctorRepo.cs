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
    public class DoctorRepo : IDoctorHalper
    {
        private HosptelWebDb hosptelWebDb;

        public DoctorRepo(HosptelWebDb hosptelWebDb) {
            this.hosptelWebDb = hosptelWebDb;
        }
        public async Task Add(Doctor talbe)
        {
            hosptelWebDb.Doctors.Add(talbe);
            await hosptelWebDb.SaveChangesAsync();
        }



        public async Task Delete(int id)
        {
            Doctor doctor = await GetById(id);
            hosptelWebDb.Doctors.Remove(doctor);
            await hosptelWebDb.SaveChangesAsync();
        }

        public async Task<List<Doctor>> GetAll()
        {
            var lst = await hosptelWebDb.Doctors.ToListAsync();
            return lst;
        }

        public async Task<Doctor> GetById(int id)
        {
            Doctor doctor = await hosptelWebDb.Doctors.FindAsync(id);
            return doctor;
        }

        public async Task Update(Doctor talbe)
        {
            hosptelWebDb = new HosptelWebDb();
            hosptelWebDb.Doctors.Update(talbe);
            await hosptelWebDb.SaveChangesAsync();
        }
    }
}
