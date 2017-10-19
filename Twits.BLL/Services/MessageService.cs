using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.BLL.DTOModels;
using Twits.BLL.CustomMappers;

namespace Twits.BLL.Services
{
    public class MessageService:Interfaces.IMessage
    {
        private DAL.Interfaces.IUnitOfWork db;

        public MessageService(DAL.Interfaces.IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<DTOViewMessage> GetAllMessages()
        {
            return db.Messages.GetAll().ToBll();
        }

        public IEnumerable<DTOViewMessage> GetAllMessagesWithTag(string tag)
        {
            var mesTags = db.Messages.GetAllQueryable().Join(db.Tags.GetAllQueryable(t => t.TagName == tag),
                m => m.Id, t => t.MessageId,
                (m, t) => m
                );

            return mesTags.AsEnumerable().ToBll();
            
        }

        public IEnumerable<DTOViewMessage> GetAllUserMessages(int userId)
        {
            return db.Messages.GetAll(m => m.UserId == userId).ToBll();
        }

        public IEnumerable<DTOViewMessage> GetFeed(int userId)
        {
            var feedMes = db.Users.GetAllQueryable(u => u.Id == userId).Join(
                    db.Subscriptions.GetAllQueryable(),
                    u => u.Id,
                    f => f.UserId,
                    (u, f) => new { u, f }
                    ).Join(db.Messages.GetAllQueryable(),
                        uf => uf.f.ReadUserId,
                        m => m.UserId,
                        (uf, m) => new DTOModels.DTOViewMessage
                        {
                            Id = m.Id,
                            DateTime = m.DateTime,
                            UserId = m.UserId
                        }
                        );

            return feedMes.AsEnumerable();
        }
    }
}
