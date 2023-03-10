using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            throw new NotImplementedException();
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

        public List<SystemLog> ListInPage(int page , string search)
        {
            var AllLog = search.IsNullOrEmpty()?ListAll():ListAll(search);
            int PageSize = 10;
            int index = (page - 1) * PageSize; 
            int Count = AllLog.Count - index>=10? PageSize:AllLog.Count-index;
            AllLog = AllLog.GetRange(index,Count);
            return AllLog;
        }

        public List<SystemLog> ListAll(string search)
        {
            if (search.IsNullOrEmpty()) return ListAll();
            string[] finding = search.Split(' ');
            var list = ListAll();
            List<SystemLog> result = new List<SystemLog>();
            foreach(var item in list)
            {
                if (finding.All(item.ToString().Contains))  result.Add(item);
            }
            return result;
        }
    }
}
