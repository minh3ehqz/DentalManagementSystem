using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DentalManagementSystem.DAL
{
    public class ExportMaterialDBContext : DBContext<MaterialExport>
    {
        public override void Add(MaterialExport entity)
        {
            MaterialExports.Add(entity);
            SaveChanges();
        }

        public override void Delete(long Id)
        {
			MaterialExports.Remove(Get(Id));
			SaveChanges();
		}

        public override MaterialExport Get(long id)
        {
            return MaterialExports.FirstOrDefault(x => x.Id == id);
        }

        public override List<MaterialExport> ListAll()
        {
            return MaterialExports.Include(x => x.Material).Where(x => !x.IsDeleted).ToList();
        }

        public override List<MaterialExport> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }
        public List<MaterialExport> ListInPage(int page, string search)
        {
            var AllMaterialExports = search.IsNullOrEmpty() ? ListAll() : ListAll(search);
            int PageSize = 10;
            int index = (page - 1) * PageSize;
            int Count = AllMaterialExports.Count - index >= 10 ? PageSize : AllMaterialExports.Count - index;
            AllMaterialExports = AllMaterialExports.GetRange(index, Count);
            return AllMaterialExports;
        }

        public List<MaterialExport> ListAll(string search)
        {
            if (search.IsNullOrEmpty()) return ListAll();
            string[] finding = search.Split(' ');
            var list = ListAll();
            List<MaterialExport> result = new List<MaterialExport>();
            foreach (var item in list)
            {
                if (finding.All(item.ToString().Contains)) result.Add(item);
            }
            return result;
        }

        public override void Update(MaterialExport entity)
        {
            MaterialExports.Update(entity);
            SaveChanges();
        }

    }
}

