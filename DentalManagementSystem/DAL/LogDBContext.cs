using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class SystemLogDBContext : DBContext<SystemLog>
    {
        public override void Add(SystemLog entity)
        {
            SystemLogs.Add(entity);
            SaveChanges();

        }

        public override void Delete(long Id)
        {

        }

        public override SystemLog Get(long id)
        {
            return SystemLogs.FirstOrDefault(x => x.Id == id);
        }

        public override List<SystemLog> ListAll()
        {
            return SystemLogs.OrderByDescending(x => x.CreatedDate).Include(x => x.Owner).ToList();
        }

        public override List<SystemLog> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }

        public override void Update(SystemLog entity)
        {
            throw new NotImplementedException();
        }
    }
}
