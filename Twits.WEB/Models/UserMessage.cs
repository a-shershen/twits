using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twits.WEB.Models
{
    public class UserMessage
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        public UserMessage OriginalMessage { get; set; }

        public int RepostCount { get; set; }
    }
}