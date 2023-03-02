using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class scheduleDBContext : DBContext<Schedule>
    {
        public override void Add(Schedule entity)
        {
            Schedules.Add(entity);
            SaveChanges();
        }

        public override void Delete(long Id)
        {
            Schedules.Remove(Schedules.First(x => x.Id == Id));
            SaveChanges();
        }

        public override Schedule Get(long id)
        {
            return Schedules.FirstOrDefault(x => x.Id == id);
        }

        public override List<Schedule> ListAll()
        {
            return Schedules.OrderBy(x => x.Date)
                .Include(x => x.Patient).ToList();
        }

        public override List<Schedule> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }

        public override void Update(Schedule entity)
        {
            throw new NotImplementedException();
        }
    }
}
