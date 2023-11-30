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
    public class EmployeeRepo : IEmployeeHalper
    {
        private readonly HosptelWebDb hosptelWebDb;

        public EmployeeRepo(HosptelWebDb hosptelWebDb)
        {
            this.hosptelWebDb = hosptelWebDb;
        }
        public async Task Add(Employee table)
        {
            hosptelWebDb.Employees.Add(table);
            await hosptelWebDb.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var emp = await GetById(id);
            hosptelWebDb.Employees.Remove(emp);
            await hosptelWebDb.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAll()
        {
            var lst = await hosptelWebDb.Employees.ToListAsync();
            return lst;
        }

        public async Task<Employee> GetById(int id)
        {
            var emp = await hosptelWebDb.Employees.FindAsync(id);
            return emp;
        }

        public async Task Update(Employee table)
        {
            hosptelWebDb.Employees.Update(table);
            await hosptelWebDb.SaveChangesAsync();
        }
    }
}
