using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            MaterialExports.Remove(MaterialExports.FirstOrDefault(x => x.Id == Id));
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

        public override void Update(MaterialExport entity)
        {
            MaterialExports.Update(entity);
            SaveChanges();
        }

    }
}

