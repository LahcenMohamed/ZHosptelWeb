using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHosptel.Models
{
    public class HosptelWebDb : IdentityDbContext<HosptelUser>
    {
        public HosptelWebDb()
        {

        }
        public HosptelWebDb(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=.\SQLEXPRESS;initial catalog=HosptelWebDB;integrated security=True;Encrypt=False;MultipleActiveResultSets=True");
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Patient> Patients {get;set;}
        public virtual DbSet<Doctor> Doctors {get;set;}
        public virtual DbSet<Room> Rooms {get;set;}
        public virtual DbSet<Employee> Employees { get;set;}
        public virtual DbSet<Medication> Medications {get;set;}
        public virtual DbSet<Reservation> Reservations {get;set;}
        public virtual DbSet<Appointment> Appointments {get;set;}
    }
}
