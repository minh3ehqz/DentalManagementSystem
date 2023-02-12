using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class ImportMaterialDBContext : DBContext<MaterialImport>
    {
        public override void Add(MaterialImport entity)
        {
            MaterialImports.Add(entity);
            SaveChanges();
        }

        public override void Delete(long Id)
        {
            MaterialImports.Remove(MaterialImports.FirstOrDefault(x => x.Id == Id));
            SaveChanges();
        }

        public override MaterialImport Get(long id)
        {
            return MaterialImports.FirstOrDefault(x => x.Id == id);
        }

        public override List<MaterialImport> ListAll()
        {
            return MaterialImports.Include(x => x.Material).Where(x => !x.IsDeleted).ToList();
        }

        public override List<MaterialImport> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }

        public override void Update(MaterialImport entity)
        {
            MaterialImports.Update(entity);
            SaveChanges();
        }

    }
}
