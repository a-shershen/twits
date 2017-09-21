using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.DAL.Models;

namespace Twits.DAL.Repos
{
    class FollowerRepo : Interfaces.IGenericRepo<Models.Follower>
    {
        private Contexts.CustomContext db;

        public FollowerRepo(Contexts.CustomContext db)
        {
            this.db = db;
        }

        public void Create(Follower item)
        {
            db.Followers.Add(item);
        }

        public void Delete(Func<Follower, bool> predicate)
        {
            var follower = db.Followers.FirstOrDefault(predicate);

            if(follower!=null)
            {
                db.Followers.Remove(follower);
            }
        }

        public IEnumerable<Follower> GetAll(Func<Follower, bool> predicate = null)
        {
            if(predicate == null)
            {
                return db.Followers.AsEnumerable();
            }
            else
            {
                return db.Followers.Where(predicate).AsEnumerable();
            }
        }

        public IQueryable<Follower> GetAllQueryable(Func<Follower, bool> predicate = null)
        {
            if (predicate == null)
            {
                return db.Followers.AsQueryable();
            }
            else
            {
                return db.Followers.Where(predicate).AsQueryable();
            }
        }

        public Follower Read(Func<Follower, bool> predicate)
        {
            return db.Followers.FirstOrDefault(predicate);
        }

        public void Update(Follower item)
        {
            db.Entry<Follower>(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
