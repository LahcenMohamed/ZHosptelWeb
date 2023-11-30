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
    public class MedicationRepo : IMedicationHalper
    {
        private readonly HosptelWebDb hosptelWebDb;

        public MedicationRepo(HosptelWebDb hosptelWebDb)
        {
            this.hosptelWebDb = hosptelWebDb;
        }
        public async Task Add(Medication talbe)
        {
            hosptelWebDb.Medications.Add(talbe);
            await hosptelWebDb.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var med = await GetById(id);
            hosptelWebDb.Medications.Remove(med);
            await hosptelWebDb.SaveChangesAsync();
        }

        public async Task<List<Medication>> GetAll()
        {
            var lst = await hosptelWebDb.Medications.ToListAsync();
            return lst;
        }

        public async Task<Medication> GetById(int id)
        {
            var mdc = await hosptelWebDb.Medications.FindAsync(id);
            return mdc;
        }

        public async Task Update(Medication talbe)
        {
            hosptelWebDb.Medications.Update(talbe);
            await hosptelWebDb.SaveChangesAsync();
        }
    }
}
