using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.BLL.DTOModels;
using Twits.BLL.CustomMappers;

namespace Twits.BLL.Services
{
    public class UserService:Interfaces.IUser
    {
        private DAL.Interfaces.IUnitOfWork db;

        public UserService(DAL.Interfaces.IUnitOfWork db)
        {
            this.db = db;
        }

        public void AddNewMessage(DTOMessage message)
        {
            foreach(var tag in message.Tags)
            {
                db.Tags.Create(new DAL.Models.Tag { MessageId = message.Id, TagName = tag });
            }

            db.Messages.Create(message.ToDal());
        }

        public void Follow(int userId, int followUserId)
        {
            db.Followers.Create(new DAL.Models.Follower { FollowerId = followUserId, UserId = userId });
        }
    }
}
