using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.BLL.Interfaces
{
    public interface IMessage
    {
        DTOModels.DTOViewMessage GetMessageById(int id);
            
        IEnumerable<DTOModels.DTOViewMessage> GetAllMessages();

        IEnumerable<DTOModels.DTOViewMessage> GetAllMessagesWithTag(string tag);

        IEnumerable<DTOModels.DTOViewMessage> GetAllUserMessages(int userId);

        IEnumerable<DTOModels.DTOViewMessage> GetFeed(int userId);

        IEnumerable<DTOModels.DTOViewMessage> GetLastMessages(int count);

        int GetRepostCount(int id);
    }
}
