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
    public class AppointmentRepo(HosptelWebDb hosptelWebDb) : IAppointmentHalper
    {
        private readonly HosptelWebDb hosptelWebDb = hosptelWebDb;

        
        public async Task Add(Appointment table)
        {
            hosptelWebDb.Appointments.Add(table);
            await hosptelWebDb.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var apt = await GetById(id);
            hosptelWebDb.Appointments.Remove(apt);
            await hosptelWebDb.SaveChangesAsync(); 
        }

        public async Task<List<Appointment>> GetAll()
        {
            var lst = await hosptelWebDb.Appointments.ToListAsync();
            return lst;
        }

        public async Task<List<Appointment>> GetAppointmentAsync(int id)
        {
            var lst = await hosptelWebDb.Appointments.Include(x => x.Patient).Where(x => x.DocterId == id).ToListAsync();
            return lst;
        }

        public async Task<List<Appointment>> GetAppointmentsPatient(int id)
        {
            var lst = await hosptelWebDb.Appointments.Include(x => x.Doctor).Where(x => x.PatientId == id).ToListAsync();
            return lst;
        }

        public async Task<Appointment> GetById(int id)
        {
            var apt = await hosptelWebDb.Appointments.FindAsync(id);
            return apt;
        }

        public async Task Update(Appointment table)
        {
            hosptelWebDb.Appointments.Update(table);
            await hosptelWebDb.SaveChangesAsync();
        }
    }
}
