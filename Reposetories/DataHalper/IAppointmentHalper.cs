using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZHosptel.Models;

namespace ZHosptel.Reposetories.DataHalper
{
    public interface IAppointmentHalper : IDataHalper<Appointment>
    {
        public Task<List<Appointment>> GetAppointmentAsync(int id);
        public Task<List<Appointment>> GetAppointmentsPatient(int id);
    }
}
