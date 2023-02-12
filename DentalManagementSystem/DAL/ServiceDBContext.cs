using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class ServiceDBContext : DBContext<Service>
    {
        public override void Add(Service entity)
        {
            Services.Add(entity);
            SaveChanges();
        }
        
        public override void Delete(long Id)
        {
            Services.Remove(Services.FirstOrDefault(x => x.Id == Id));
            SaveChanges();
        }

        public override Service Get(long id)
        {
            return Services
                .Include(x => x.ServiceRecords)
                .Include(x => x.Treatments)
                .Include(x => x.Schedules)
                .FirstOrDefault(x => x.Id == id);
        }

        public override List<Service> ListAll()
        {
            return Services
                .Where(x => !x.IsDeleted).ToList();
        }

        public override List<Service> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }

        public override void Update(Service entity)
        {
            Services.Update(entity);
            SaveChanges();
        }
    }
}
