using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class PatientDBContext : DBContext<Patient>
    {
        public List<Patient> GetByGender(bool gender, string name)
        {
            var AllPatients = Patients.Where(x => x.Gender == gender && x.Name == name).ToList();
            int page = 10;
            int PageSize = 10;
            int index = (page - 1) * PageSize;
            int ResultCount = AllPatients.Count - index >= 10 ? index + PageSize : AllPatients.Count - index;
            AllPatients = AllPatients.GetRange(index, ResultCount);
            return AllPatients;
        }

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
