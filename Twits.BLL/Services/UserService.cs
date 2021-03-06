﻿using System;
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

            db.Save();
        }

        public void DeleteMessage(int id)
        {
            db.Messages.Delete(m => m.OriginalMessageId == id);
            db.Messages.Delete(m => m.Id == id);
            db.Save();
        }

        public void Follow(int userId, int followUserId)
        {
            db.Subscriptions.Create(new DAL.Models.Subscription { ReadUserId = followUserId, UserId = userId });
        }

        public IEnumerable<string> GetSubscribers(string user)
        {
            var subscribers = db.Users.GetAll(u => u.Login == user).Join(db.Subscriptions.GetAll(), u => u.Id,
                s => s.ReadUserId,
                (u, s) => s.UserId)
                .Join(db.Users.GetAll(), o => o, i => i.Id, (o, i) => i.Login);

            return subscribers;
        }

        public IEnumerable<string> GetSubscriptions(string user)
        {
            var subscriptions = db.Users.GetAll(u => u.Login == user).Join(db.Subscriptions.GetAll(), u => u.Id,
                s => s.UserId,
                (u, s) => s.ReadUserId)
                .Join(db.Users.GetAll(), o => o, i => i.Id, (o, i) => i.Login);

            return subscriptions;


        }

        public int GetUserIdByName(string userName)
        {
            var user = db.Users.Read(u => u.Login == userName);

            if(user!=null)
            {
                return user.Id;
            }
            else
            {
                return -1;
            }
        }

        public string GetUserNameById(int id)
        {
            var user = db.Users.Read(u => u.Id == id);

            if(user!=null)
            {
                return user.Login;
            }
            else
            {
                return null;
            }
        }

        public bool IsRepostAlreadyMade(string user, int messageId)
        {
            try
            {
                var userId = db.Users.Read(u => u.Login == user).Id;
                var message = db.Messages.Read(m => m.OriginalMessageId == messageId && m.UserId == userId);

                //message is null when repostMessage doesn't exist
                if (message == null)
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }


            catch
            {
                return false;
            }
        }

        public bool IsSubscribed(string user, string subscribed)
        {
            try
            {
                int userId = db.Users.Read(u => u.Login == user).Id;

                int subscrId = db.Users.Read(u => u.Login == subscribed).Id;

                var sub = db.Subscriptions.Read(s => s.UserId == userId && s.ReadUserId == subscrId);

                if(sub != null)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            catch
            {
                return false;
            }
        }

        public void Subscribe(string user, string subscription)
        {
            try
            {
                int userId = db.Users.Read(u => u.Login == user).Id;

                int subId = db.Users.Read(u => u.Login == subscription).Id;

                db.Subscriptions.Create(new DAL.Models.Subscription { UserId = userId, ReadUserId = subId });
                db.Save();
            }
            catch
            {
                
            }
        }

        public void Unsubscribe(string user, string subscription)
        {
            try
            {
                int userId = db.Users.Read(u => u.Login == user).Id;

                int subId = db.Users.Read(u => u.Login == subscription).Id;

                db.Subscriptions.Delete(s => s.UserId == userId && s.ReadUserId == subId);

                db.Save();
            }
            
            catch
            {

            }
        }
    }
}
