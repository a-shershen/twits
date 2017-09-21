using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.DAL.Models;

namespace Twits.DAL.Repos
{
    class RolesRepo : Interfaces.IGenericRepo<Models.Role>
    {
        private Contexts.CustomContext db;

        public RolesRepo(Contexts.CustomContext db)
        {
            throw new NotImplementedException();
        }

        public void Create(Role item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Func<Role, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll(Func<Role, bool> predicate = null)
        {
            if(predicate!=null)
            {
                return db.Roles.Where(predicate).AsEnumerable();
            }

            else
            {
                return db.Roles.AsEnumerable();
            }
        }

        public IQueryable<Role> GetAllQueryable(Func<Role, bool> predicate = null)
        {
            if (predicate == null)
            {
                return db.Roles.AsQueryable();
            }
            else
            {
                return db.Roles.Where(predicate).AsQueryable();
            }
        }

        public Role Read(Func<Role, bool> predicate)
        {
            return db.Roles.FirstOrDefault(predicate);
        }

        public void Update(Role item)
        {
            throw new NotImplementedException();
        }
    }
}
