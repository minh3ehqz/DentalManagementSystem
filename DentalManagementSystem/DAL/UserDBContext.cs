using DentalManagementSystem.Models;

namespace DentalManagementSystem.DAL
{
	public class UserDBContext : DBContext<User>
	{
		public User GetByUsernamePassword(string username, string password)
		{
			return Users.FirstOrDefault(x => x.Username == username && x.Password == password);
		}

        public override void Add(User entity)
		{
			Users.Add(entity);
			SaveChanges();
		}

		public override void Delete(long Id)
		{
			Users.Remove(Users.First(x => x.Id == Id));
			SaveChanges();
		}

		public override User Get(long id)
		{
			return Users.FirstOrDefault(x => x.Id == id);
		}

		public override List<User> ListAll()
		{
			return Users.ToList();
		}

		public override List<User> ListAll(long OwnerId)
		{
			throw new NotImplementedException();
		}

		public override void Update(User entity)
		{
            Users.Update(entity);
            SaveChanges();
        }
	}
}
