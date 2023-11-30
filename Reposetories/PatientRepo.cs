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
    public class PatientRepo : IPetienHalper
    {
        private readonly HosptelWebDb hosptelWebDb;

        public PatientRepo(HosptelWebDb hosptelWebDb)
        {
            this.hosptelWebDb = hosptelWebDb;
        }
        public async Task Add(Patient talbe)
        {
            hosptelWebDb.Patients.Add(talbe);
            await hosptelWebDb.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var pat = await GetById(id);
            hosptelWebDb.Patients.Remove(pat);
            await hosptelWebDb.SaveChangesAsync();
        }

        public async Task<List<Patient>> GetAll()
        {
            var lst = await hosptelWebDb.Patients.ToListAsync();
            return lst;
        }

        public async Task<Patient> GetById(int id)
        {
            Patient patient = await hosptelWebDb.Patients.FindAsync(id);
            return patient;
        }

        public async Task Update(Patient talbe)
        {
            hosptelWebDb.Patients.Update(talbe);
            await hosptelWebDb.SaveChangesAsync();
        }
    }
}
