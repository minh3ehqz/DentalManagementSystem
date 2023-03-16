using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            Patients.Remove(Get(Id));
            SaveChanges();
        }

        public override Patient Get(long id)
        {
            return Patients
                .Include(x => x.Treatments).ThenInclude(y => y.TreatmentServiceMaps)
                .Include(x => x.PatientRecords).ThenInclude(y => y.PatientRecordServiceMaps)
                .Include(x => x.PatientRecords).ThenInclude(y=>y.TreatmentServiceMaps)
                .Include(x => x.PatientRecords).ThenInclude(y => y.MaterialExports)
                .Include(x => x.Schedules)
                .FirstOrDefault(x => x.Id == id);
        }

        public override List<Patient> ListAll()
        {
            return Patients
                .Where(x => !x.IsDeleted).ToList();
        }

        public List<Patient> ListInPage(int page, string search)
        {
            var AllPatients = search.IsNullOrEmpty() ? ListAll() : ListAll(search);
            int PageSize = 10;
            int index = (page - 1) * PageSize;
            int Count = AllPatients.Count - index >= 10 ? PageSize : AllPatients.Count - index;
            AllPatients = AllPatients.GetRange(index, Count);
            return AllPatients;
        }

        public List<Patient> ListAll(string search)
        {
            if (search.IsNullOrEmpty()) return ListAll();
            string[] finding = search.Split(' ');
            var list = ListAll();
            List<Patient> result = new List<Patient>();
            foreach (var item in list)
            {
                if (finding.All(item.ToString().Contains)) result.Add(item);
            }
            return result;
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
