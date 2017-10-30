using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.BLL.Interfaces
{
    public interface IUser
    {
        void AddNewMessage(DTOModels.DTOMessage message);

        void Follow(int userId, int followUserId);

        int GetUserIdByName(string userName);

        string GetUserNameById(int id);

        IEnumerable<string> GetSubscriptions(string user);

        IEnumerable<string> GetSubscribers(string user);

        bool IsSubscribed(string user, string subscribed);

        void Subscribe(string user, string subscription);

        void Unsubscribe(string user, string subscription);

        bool IsRepostAlreadyMade(string user, int messageId);
    }
}
