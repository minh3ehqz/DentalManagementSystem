using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class LogDBContext : DBContext<Log>
    {
        public override void Add(Log entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(long Id)
        {
           
        }

        public override Log Get(long id)
        {
            return null;
        }

        public override List<Log> ListAll()
        {
            return null;
        }

        public override List<Log> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }

        public override void Update(Log entity)
        {
           
        }
    }
}
