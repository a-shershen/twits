using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.DAL.Models;

namespace Twits.DAL.Repos
{
    class UserRepo : Interfaces.IGenericRepo<Models.User>
    {
        private Contexts.CustomContext db;

        public UserRepo(Contexts.CustomContext db)
        {
            this.db = db;
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(Func<User, bool> predicate)
        {
            var user = Read(predicate);

            if(user!=null)
            {
                db.Users.Remove(user);
            }
        }

        public IEnumerable<User> GetAll(Func<User, bool> predicate = null)
        {
            if(predicate!=null)
            {
                return db.Users.Where(predicate).AsEnumerable();
            }
            else
            {
                return db.Users.AsEnumerable();
            }
        }

        public User Read(Func<User, bool> predicate)
        {
            return db.Users.FirstOrDefault(predicate);
        }

        public void Update(User item)
        {
            db.Entry<User>(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
