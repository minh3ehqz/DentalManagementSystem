using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public List<Schedule> ListInPage(int page, string search ,int status=0)
        {
            var AllSchedules =  ListAll(search,status);
            int PageSize = 10;
            int index = (page - 1) * PageSize;
            int Count = AllSchedules.Count - index >= 10 ? PageSize : AllSchedules.Count - index;
            AllSchedules = AllSchedules.GetRange(index, Count);
            return AllSchedules;
        }

        public List<Schedule> ListAll(string search ,int status=0)
        {
            if (search.IsNullOrEmpty())
            {
                return Schedules.Where(x =>x.Status == status).Include(x =>x.Patient).OrderBy(x => x.Date).ToList();
            }
            string[] finding = search.Split(' ');
            var list = ListAll();
            List<Schedule> result = new List<Schedule>();
            foreach (var item in list)
            {
                if (finding.All(item.ToString().Contains) && item.Status==status) result.Add(item);
            }
            return result;
        }

        public override List<Schedule> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }

        public override void Update(Schedule entity)
        {
            Schedules.Update(entity);
            SaveChanges();
        }
    }
}
