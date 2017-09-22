using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.BLL.DTOModels
{
    public class DTOMessage
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime DateTime { get; set; }

        public string Text { get; set; }

        public List<string> Tags { get; set; }
    }
}
