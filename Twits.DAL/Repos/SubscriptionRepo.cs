using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.DAL.Models;

namespace Twits.DAL.Repos
{
    class SubscriptionRepo : Interfaces.IGenericRepo<Models.Subscription>
    {
        private Contexts.CustomContext db;

        public SubscriptionRepo(Contexts.CustomContext db)
        {
            this.db = db;
        }

        public void Create(Subscription item)
        {
            db.Subscriptions.Add(item);
        }

        public void Delete(Func<Subscription, bool> predicate)
        {
            var follower = db.Subscriptions.FirstOrDefault(predicate);

            if(follower!=null)
            {
                db.Subscriptions.Remove(follower);
            }
        }

        public IEnumerable<Subscription> GetAll(Func<Subscription, bool> predicate = null)
        {
            if(predicate == null)
            {
                return db.Subscriptions.AsEnumerable();
            }
            else
            {
                return db.Subscriptions.Where(predicate).AsEnumerable();
            }
        }

        public IQueryable<Subscription> GetAllQueryable(Func<Subscription, bool> predicate = null)
        {
            if (predicate == null)
            {
                return db.Subscriptions.AsQueryable();
            }
            else
            {
                return db.Subscriptions.Where(predicate).AsQueryable();
            }
        }

        public Subscription Read(Func<Subscription, bool> predicate)
        {
            return db.Subscriptions.FirstOrDefault(predicate);
        }

        public void Update(Subscription item)
        {
            db.Entry<Subscription>(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
