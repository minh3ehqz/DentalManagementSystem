using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class TimeKeepingDBContext : DBContext<Timekeeping>
    {
        public override void Add(Timekeeping entity)
        {
            var keeping = Timekeepings.FirstOrDefault(x=>x.UserId== entity.UserId && x.TimeCheckin == entity.TimeCheckin);
            if (keeping == null)
            {
                Timekeepings.Add(entity);
                SaveChanges();
            }
            else
            {
                Update(entity);
            }
        }

        public override void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public override Timekeeping Get(long id)
        {
            return Timekeepings.OrderBy(x=>x.Id).LastOrDefault(x=>x.UserId==id);
        }

        public Timekeeping get(long id)
        {
            return Timekeepings.FirstOrDefault(x=>x.Id==id);
        }

        public override List<Timekeeping> ListAll()
        {
            return Timekeepings.Include(x=>x.User).ToList();
        }

        public override List<Timekeeping> ListAll(long OwnerId)
        {
            return Timekeepings.Where(x=>x.UserId== OwnerId).OrderByDescending(x=>x.TimeCheckin).ToList();
        }

        public override void Update(Timekeeping entity)
        {
            Timekeepings.Update(entity); 
            SaveChanges();
        }
    }
}
