using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class PatientRecordDBContext : DBContext<PatientRecord>
    {
        public override void Add(PatientRecord entity)
        {
            PatientRecords.Add(entity);
            SaveChanges();
        }

        public override void Delete(long Id)
        {
            PatientRecords.Remove(PatientRecords.FirstOrDefault(x => x.Id == Id));
            SaveChanges();
        }

        public override PatientRecord Get(long id)
        {
            return PatientRecords.Include(y => y.PatientRecordServiceMaps)
                .Include(y => y.TreatmentServiceMaps)
                .Include(y => y.MaterialExports)
                .FirstOrDefault(x => x.Id == id);
        }

        public override List<PatientRecord> ListAll()
        {
            return PatientRecords.Include(x => x.Patient)
                .Include(x => x.PatientRecordServiceMaps).ThenInclude(x => x.Service).ToList();
        }

        public override List<PatientRecord> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }

        public override void Update(PatientRecord entity)
        {
            PatientRecords.Update(entity);
            SaveChanges();
        }
    }
}
