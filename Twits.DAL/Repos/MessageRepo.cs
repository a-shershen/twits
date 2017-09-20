using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.DAL.Models;

namespace Twits.DAL.Repos
{
    class MessageRepo : Interfaces.IGenericRepo<Models.Message>
    {
        private Contexts.CustomContext db;
        
        public MessageRepo(Contexts.CustomContext db)
        {
            this.db = db;
        }

        public void Create(Message item)
        {
            db.Messages.Add(item);
        }

        public void Delete(Func<Message, bool> predicate)
        {
            var message = db.Messages.FirstOrDefault(predicate);

            if(message != null)
            {
                db.Messages.Remove(message);
            }
        }

        public IEnumerable<Message> GetAll(Func<Message, bool> predicate = null)
        {
            if(predicate != null)
            {
                return db.Messages.Where(predicate).AsEnumerable();
            }
            else
            {
                return db.Messages.AsEnumerable();
            }
        }

        public Message Read(Func<Message, bool> predicate)
        {
            return db.Messages.FirstOrDefault(predicate);
        }

        public void Update(Message item)
        {
            db.Entry<Message>(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
