using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twits.DAL.Models
{
    public class Follower
    {
        public int id { get; set; }

        public int UserId { get; set; }

        public int FollowerId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("FollowerId")]
        public User UserFollower { get; set; }
    }
}
