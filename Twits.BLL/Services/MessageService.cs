using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.BLL.DTOModels;
using Twits.BLL.CustomMappers;

namespace Twits.BLL.Services
{
    public class MessageService : Interfaces.IMessage
    {
        private DAL.Interfaces.IUnitOfWork db;

        public MessageService(DAL.Interfaces.IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<DTOViewMessage> GetAllMessages()
        {
            return db.Messages.GetAll().Join(db.Users.GetAll(), m => m.UserId, u => u.Id,
                (m, u) => new DTOModels.DTOViewMessage
                {
                    Id = m.Id,
                    OriginalMessageId = m.OriginalMessageId,
                    RepostCount = GetRepostCount(m.Id),
                    DateTime = m.DateTime,
                    Text = m.Text,
                    UserId = u.Id,
                    Login = u.Login
                });
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
            return db.Messages.GetAll().Join(db.Users.GetAll(u => u.Id == userId), m => m.UserId, u => u.Id,
                (m, u) => new DTOModels.DTOViewMessage
                {
                    Id = m.Id,
                    OriginalMessageId = m.OriginalMessageId,
                    RepostCount = GetRepostCount(m.Id),
                    DateTime = m.DateTime,
                    Text = m.Text,
                    UserId = u.Id,
                    Login = u.Login
                });

        }

        public IEnumerable<DTOViewMessage> GetFeed(int userId)
        {
            var feedMes = db.Users.GetAll(u => u.Id == userId).Join(
                    db.Subscriptions.GetAll(),
                    u => u.Id,
                    f => f.UserId,
                    (u, f) => new { u, f }
                    ).Join(db.Messages.GetAll(),
                        uf => uf.f.ReadUserId,
                        m => m.UserId,
                        (uf, m) => new DTOModels.DTOViewMessage
                        {
                            Id = m.Id,
                            OriginalMessageId = m.OriginalMessageId,
                            DateTime = m.DateTime,
                            UserId = m.UserId,
                            Login = uf.f.ReadUser.Login,
                            Text = m.Text
                        }
                        ).OrderByDescending(x => x.DateTime);

            return feedMes;
        }

        public IEnumerable<DTOViewMessage> GetLastMessages(int count)
        {
            return db.Messages.GetAllQueryable().OrderByDescending(m => m.DateTime).Take(count)
                .Join(db.Users.GetAll(), m => m.UserId, u => u.Id,
                (m, u) => new DTOModels.DTOViewMessage
                {
                    Id = m.Id,
                    OriginalMessageId = m.OriginalMessageId,
                    DateTime = m.DateTime,
                    Text = m.Text,
                    UserId = u.Id,
                    Login = u.Login
                });
        }

        public DTOViewMessage GetMessageById(int id)
        {
            var mes = db.Messages.Read(m => m.Id == id);

            if(mes!=null)
            {
                return mes.ToBll();
            }
            else
            {
                return null;
            }
        }

        public int GetRepostCount(int id)
        {
            var reposts = db.Messages.GetAll(m => m.OriginalMessageId == id);

            if(reposts!=null)
            {
                return reposts.Count();
            }
            else
            {
                return 0;
            }
        }
    }
}

