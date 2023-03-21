using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DentalManagementSystem.DAL
{
	public class MaterialDBContext : DBContext<Material>
	{
		public override void Add(Material entity)
		{
			Materials.Add(entity);
			SaveChanges();
		}

		public Material getName(string name)
		{
			return Materials.FirstOrDefault(x => x.Name.Equals(name));
		}
		public int getAmount(long id)
		{
			var material = Get(id);
			return material.Amount;
		}
		public int getPrice(long id)
		{
			var material = Get(id);
			return material.Price;
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
		public List<Material> ListInPage(int page, string search)
		{
			var AllMaterials = search.IsNullOrEmpty() ? ListAll() : ListAll(search);
			int PageSize = 10;
			int index = (page - 1) * PageSize;
			int Count = AllMaterials.Count - index >= 10 ? PageSize : AllMaterials.Count - index;
			AllMaterials = AllMaterials.GetRange(index, Count);
			return AllMaterials;
		}

		public List<Material> ListAll(string search)
		{
			if (search.IsNullOrEmpty()) return ListAll();
			string[] finding = search.Split(' ');
			var list = ListAll();
			List<Material> result = new List<Material>();
			foreach (var item in list)
			{
				if (finding.All(item.ToString().Contains)) result.Add(item);
			}
			return result;
		}

		public override void Update(Material entity)
		{
			Materials.Update(entity);
			SaveChanges();
		}		
	}
}
