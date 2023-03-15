using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class TreatmentsDBContext : DBContext<Treatment>
    {
        public override void Add(Treatment entity)
        {
            Treatments.Add(entity);
            SaveChanges();
        }

        public override void Delete(long Id)
        {
            Treatments.Remove(Get(Id));
            SaveChanges();
        }

        public override Treatment Get(long id)
        {
            return Treatments.Include(x=>x.TreatmentServiceMaps).FirstOrDefault(x => x.Id == id);
        }

        public override List<Treatment> ListAll()
        {
            throw new NotImplementedException();
        }

        public override List<Treatment> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }

        public override void Update(Treatment entity)
        {
            throw new NotImplementedException();
        }
    }
}
