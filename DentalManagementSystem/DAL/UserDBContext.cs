using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DentalManagementSystem.DAL
{
	public class UserDBContext : DBContext<User>
	{
		public User GetByUsernamePassword(string username, string password)
		{
			return Users.FirstOrDefault(x => x.Username == username && x.Password == password);
		}

	
        public User GetUsersByEmail(string email)
        {
            return Users.FirstOrDefault(x => x.Email == email);
        }
        public User GetUsersByPhone(string phone)
        {
            return Users.FirstOrDefault(x => x.Phone == phone);
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
			return Users
				.Include(x=>x.PatientRecords).ThenInclude(a=>a.PatientRecordServiceMaps)
				.Include(x=>x.SystemLogs)
				.Include(x=>x.Timekeepings)
				.FirstOrDefault(x => x.Id == id);
		}

        public List<User> ListInPage(int page, string search)
        {
            var AllUsers = search.IsNullOrEmpty() ? ListAll() : ListAll(search);
            int PageSize = 10;
            int index = (page - 1) * PageSize;
            int Count = AllUsers.Count - index >= 10 ? PageSize : AllUsers.Count - index;
            AllUsers = AllUsers.GetRange(index, Count);
            return AllUsers;
        }

        public List<User> ListAll(string search)
        {
            if (search.IsNullOrEmpty()) return ListAll();
            string[] finding = search.Split(' ');
            var list = ListAll();
            List<User> result = new List<User>();
            foreach (var item in list)
            {
                if (finding.All(item.ToString().Contains)) result.Add(item);
            }
            return result;
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
