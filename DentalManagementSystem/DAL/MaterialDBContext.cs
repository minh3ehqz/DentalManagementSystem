using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
	public class MaterialDBContext : DBContext<Material>
	{
		public override void Add(Material entity)
		{
			Materials.Add(entity);
			SaveChanges();
		}

		public override void Delete(long Id)
		{
			Materials.Remove(Materials.FirstOrDefault(x => x.Id == Id));
			SaveChanges();
		}

		public override Material Get(long id)
		{
			return Materials.FirstOrDefault(x => x.Id == id);
		}

		public override List<Material> ListAll()
		{
			return Materials.ToList();
		}

		public override List<Material> ListAll(long OwnerId)
		{
			throw new NotImplementedException();
		}

		public override void Update(Material entity)
		{
			Materials.Update(entity);
			SaveChanges();
		}		
	}
}
