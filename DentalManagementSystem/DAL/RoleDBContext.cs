using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
	public class RoleDBContext: DBContext<Role>
	{
		public RoleDBContext() 
		{
		}

		public override void Add(Role entity)
		{
            Roles.Add(entity);
            SaveChanges();
        }

		public override void Delete(long Id)
		{
            Roles.Remove(Roles.FirstOrDefault(x => x.Id == Id));
            SaveChanges(); ;
		}

		public override Role Get(long id)
		{
            return Roles.FirstOrDefault(x => x.Id == id); ;
		}

		public override List<Role> ListAll()
        {
            return Roles.ToList(); ;
		}

		public override List<Role> ListAll(long OwnerId)
		{
			throw new NotImplementedException();
		}

		public override void Update(Role entity)
		{

            Roles.Update(entity);
            SaveChanges(); ;
		}
	}
}
