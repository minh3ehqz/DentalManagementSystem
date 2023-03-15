using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class PatientRecordServiceMapDBContext : DBContext<PatientRecordServiceMap>
    {
        public override void Add(PatientRecordServiceMap entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public override PatientRecordServiceMap Get(long id)
        {
            throw new NotImplementedException();
        }

        public override List<PatientRecordServiceMap> ListAll()
        {
            throw new NotImplementedException();
        }

        public override List<PatientRecordServiceMap> ListAll(long OwnerId)
        {
            return PatientRecordServiceMaps
                .Where(x =>x.PatientRecordId== OwnerId)
                .Include(x => x.Service).ToList();
        }

        public override void Update(PatientRecordServiceMap entity)
        {
            throw new NotImplementedException();
        }
    }
}
