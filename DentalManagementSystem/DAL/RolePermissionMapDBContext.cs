using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DentalManagementSystem.DAL
{
    public class RolePermissionMapDBContext : DBContext<RolePermissionMap>
    {
        public override void Add(RolePermissionMap entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public override RolePermissionMap Get(long id)
        {
            throw new NotImplementedException();
        }

        public override List<RolePermissionMap> ListAll()
        {
            return RolePermissionMaps.Include(x => x.Permission).ToList();
        }

        public override List<RolePermissionMap> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }

        public override void Update(RolePermissionMap entity)
        {
            throw new NotImplementedException();
        }
    }
}
