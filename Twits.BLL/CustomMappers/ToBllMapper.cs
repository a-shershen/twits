using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.BLL.CustomMappers
{
    public static class ToBllMapper
    {
        public static BLL.DTOModels.DTOViewMessage ToBll(this DAL.Models.Message mes)
        {
            return new DTOModels.DTOViewMessage
            {
                Id = mes.Id,
                DateTime = mes.DateTime,
                UserId = mes.UserId
            };
        }

        public static IEnumerable<BLL.DTOModels.DTOViewMessage> ToBll(this IEnumerable<DAL.Models.Message> dalMessages)
        {
            List<DTOModels.DTOViewMessage> dtoMessages = new List<DTOModels.DTOViewMessage>();

            foreach(var m in dalMessages)
            {
                dtoMessages.Add(m.ToBll());
            }

            return dtoMessages.AsEnumerable();
        }

    }
}
