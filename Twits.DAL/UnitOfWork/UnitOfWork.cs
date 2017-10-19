using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.DAL.Interfaces;
using Twits.DAL.Models;

namespace Twits.DAL.UnitOfWork
{
    public class UnitOfWork : Interfaces.IUnitOfWork
    {
        private Contexts.CustomContext db;

        private IGenericRepo<Models.Subscription> subscriptionsRepo;
        private IGenericRepo<Models.Message> messagesRepo;
        private IGenericRepo<Models.Role> rolesRepo;
        private IGenericRepo<Models.Tag> tagsRepo;
        private IGenericRepo<Models.User> usersRepo;

        public UnitOfWork(string connectionString)
        {
            db = new Contexts.CustomContext(connectionString);
        }

        

        public IGenericRepo<Subscription> Subscriptions
        {
            get
            {
                if (subscriptionsRepo == null)
                    subscriptionsRepo = new Repos.SubscriptionRepo(db);

                return subscriptionsRepo;
            }
        }

        public IGenericRepo<Message> Messages
        {
            get
            {
                if (messagesRepo == null)
                    messagesRepo = new Repos.MessageRepo(db);

                return messagesRepo;
            }
        }

        public IGenericRepo<Role> Roles
        {
            get
            {
                if (rolesRepo == null)
                    rolesRepo = new Repos.RolesRepo(db);

                return rolesRepo;
            }
        }

        public IGenericRepo<Tag> Tags
        {
            get
            {
                if (tagsRepo == null)
                    tagsRepo = new Repos.TagRepo(db);

                return tagsRepo;
            }
        }

        public IGenericRepo<User> Users
        {
            get
            {
                if (usersRepo == null)
                    usersRepo = new Repos.UserRepo(db);

                return usersRepo;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
