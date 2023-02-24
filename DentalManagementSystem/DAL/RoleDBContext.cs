using DentalManagementSystem.Models;

namespace DentalManagementSystem.DAL
{
	public class RoleDBContext: DBContext<Role>
	{
		public RoleDBContext() 
		{
		}

		public override void Add(Role entity)
		{
			throw new NotImplementedException();
		}

		public override void Delete(long Id)
		{
			throw new NotImplementedException();
		}

		public override Role Get(long id)
		{
			throw new NotImplementedException();
		}

		public override List<Role> ListAll()
		{
			throw new NotImplementedException();
		}

		public override List<Role> ListAll(long OwnerId)
		{
			throw new NotImplementedException();
		}

		public override void Update(Role entity)
		{
			throw new NotImplementedException();
		}
	}
}
