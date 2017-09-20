using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.DAL.Models;

namespace Twits.DAL.Repos
{
    class TagRepo : Interfaces.IGenericRepo<Models.Tag>
    {
        private Contexts.CustomContext db;

        public TagRepo(Contexts.CustomContext db)
        {
            this.db = db;
        }

        public void Create(Tag item)
        {
            db.Tags.Add(item);
        }

        public void Delete(Func<Tag, bool> predicate)
        {
            var tag = Read(predicate);

            if(tag!=null)
            {
                db.Tags.Remove(tag);
            }
        }

        public IEnumerable<Tag> GetAll(Func<Tag, bool> predicate = null)
        {
            if(predicate!=null)
            {
                return db.Tags.Where(predicate).AsEnumerable();
            }
            else
            {
                return db.Tags.AsEnumerable();
            }
        }

        public Tag Read(Func<Tag, bool> predicate)
        {
            return db.Tags.FirstOrDefault(predicate);
        }

        public void Update(Tag item)
        {
            db.Entry<Tag>(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
