using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepo<Models.Subscription> Subscriptions { get; }
        IGenericRepo<Models.Message> Messages { get; }
        IGenericRepo<Models.Role> Roles { get; }
        IGenericRepo<Models.Tag> Tags { get; }
        IGenericRepo<Models.User> Users { get; }

        void Save();
    }
}
