using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class PatientDBContext : DBContext<Patient>
    {
        public Patient GetPatientsByEmail(string email)
        {
            return Patients.FirstOrDefault(x => x.Email == email);
        }
        public Patient GetPatientsByPhone(string phone)
        {
            return Patients.FirstOrDefault(x => x.Phone == phone);
        }

        public override void Add(Patient entity)
        {
            Patients.Add(entity);
            SaveChanges();
        }
        
        public override void Delete(long Id)
        {
            PatientRecordDBContext patientsRecord = new PatientRecordDBContext();
            Patients.Remove(Get(Id));
            SaveChanges();
        }

        public override Patient Get(long id)
        {
            return Patients
                .Include(x => x.PatientRecords)
                .Include(x => x.Treatments)
                .Include(x => x.Schedules)
                .FirstOrDefault(x => x.Id == id);
        }

        public override List<Patient> ListAll()
        {
            return Patients
                .Where(x => !x.IsDeleted).ToList();
        }

        public override List<Patient> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }

        public override void Update(Patient entity)
        {
            Patients.Update(entity);
            SaveChanges();
        }
    }
}
