using System.IdentityModel.Tokens.Jwt;
using ZHosptel.Models;

namespace ZHosptelWeb.DTOs
{
    public class DTOsConverter
    {
        public static async Task<Doctor> ConvertToDoctor(DoctorDto doctorDto, string? imgUrl)
        {
            var doct = new Doctor()
            {
                Id = doctorDto.Id,
                FirstName = doctorDto.FirstName,
                LastName = doctorDto.LastName,
                Address = doctorDto.Address,
                Email = doctorDto.Email,
                PhoneNumber = doctorDto.PhoneNumber,
                Gender = doctorDto.Gender,
                Specialty = doctorDto.Specialty,
                ImageUrl = imgUrl,
                DayHours = doctorDto.DayHours,
                OtherCredentials = doctorDto.OtherCredentials,
                DateOfBirth = new DateOnly(doctorDto.DateOfBirth.Year, doctorDto.DateOfBirth.Month, doctorDto.DateOfBirth.Day)

            };
            return doct;
        }
        public static async Task<Patient> ConvertToPatient(PatientDTO patientDTO)
        {
            var pat = new Patient() {
                Id = patientDTO.Id,
                FirstName = patientDTO.FirstName,
                LastName = patientDTO.LastName,
                Address = patientDTO.Address,
                Email = patientDTO.Email,
                PhoneNumber = patientDTO.PhoneNumber,
                Gender = patientDTO.Gender,
                DateOfBirth = new DateOnly(patientDTO.DateOfBirth.Year, patientDTO.DateOfBirth.Month, patientDTO.DateOfBirth.Day)
            };
            return pat;
        }
        public static async Task<Employee> ConvertToEmployee(EmployeeDTO employeeDTO)
        {
            var pat = new Employee()
            {
                Id = employeeDTO.Id,
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                Address = employeeDTO.Address,
                Email = employeeDTO.Email,
                PhoneNumber = employeeDTO.PhoneNumber,
                JobTitle = employeeDTO.JobTitle,
                DateOfBirth = new DateOnly(employeeDTO.DateOfBirth.Year, employeeDTO.DateOfBirth.Month, employeeDTO.DateOfBirth.Day)
            };
            return pat;
        }
        public static async Task<Medication> ConvertToMedication(MedicationDTO medicationDTO,string imgUrl)
        {
            var med = new Medication() 
            {
                Id = medicationDTO.Id,
                Name = medicationDTO.Name,
                Price = medicationDTO.Price,
                Description = medicationDTO.Description,
                ImageUrl = imgUrl
            };
            return med;
        }
        public static async Task<Reservation> ConvertToReservation(ReservationDTO reservationDTO)
        {
            var reservation = new Reservation() 
            {
                Id = reservationDTO.Id,
                PatientId = reservationDTO.PatientId,
                RoomId = reservationDTO.RoomId,
                DateOfReservation = new DateOnly(reservationDTO.DateOfReservation.Year, reservationDTO.DateOfReservation.Month, reservationDTO.DateOfReservation.Day),
                TimeOfReservation = new TimeOnly(reservationDTO.TimeOfReservation.Hour, reservationDTO.TimeOfReservation.minut, reservationDTO.TimeOfReservation.sconde)
            };
            return reservation;
        }
        public static async Task<Appointment> ConvertToAppointment(AppointmentDTO appointmentDTO)
        {
            var appointment = new Appointment()
            {
                Id = appointmentDTO.Id,
                PatientId = appointmentDTO.PatientId,
                DocterId = appointmentDTO.DocterId,
                DateOfAppointment = new DateOnly(appointmentDTO.DateOfAppointment.Year, appointmentDTO.DateOfAppointment.Month, appointmentDTO.DateOfAppointment.Day),
                TimeOfAppointment = new TimeOnly(appointmentDTO.TimeOfAppointment.Hour, appointmentDTO.TimeOfAppointment.minut, appointmentDTO.TimeOfAppointment.sconde)
            };
            return appointment;
        }
    }
}
