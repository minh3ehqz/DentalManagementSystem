using DentalManagementSystem.Models;

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
            throw new NotImplementedException();
        }

        public override Treatment Get(long id)
        {
            throw new NotImplementedException();
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
