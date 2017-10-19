using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace Twits.DAL.Contexts
{
    class CustomContext : DbContext
    {
        public CustomContext(string connectionString) : base(connectionString)
        {
            //OnModelCreating(new DbModelBuilder());
            Database.SetInitializer<CustomContext>(new DBInitializer());
        }

        public DbSet<Models.Subscription> Subscriptions { get; set; }
        public DbSet<Models.Message> Messages { get; set; }
        public DbSet<Models.Role> Roles { get; set; }
        public DbSet<Models.Tag> Tags { get; set; }
        public DbSet<Models.User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Subscription>()
                .HasRequired(s => s.User)
                .WithMany(u => u.SubscriptionsUsers)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Models.Subscription>()
                .HasRequired(s => s.ReadUser)
                .WithMany(u => u.SubscriptionsReadUsers)
                .HasForeignKey(s => s.ReadUserId)
                .WillCascadeOnDelete(false);
                
        }        
    }

    class DBInitializer:DropCreateDatabaseIfModelChanges<CustomContext>
    {
        protected override void Seed(CustomContext context)
        {
            context.Roles.Add(new Models.Role
            {
                RoleName = "admin"
            });

            context.Roles.Add(new Models.Role
            {
                RoleName = "moderator"
            });

            context.Roles.Add(new Models.Role
            {
                RoleName = "user"
            });

            context.Users.Add(new Models.User
            {
                Login = "admin",
                Password = "96cae35ce8a9b0244178bf28e4966c2ce1b8385723a96a6b838858cdd6ca0a1e",
                RoleId = 1
            });

            context.Users.Add(new Models.User
            {
                Login = "moderator",
                Password = "96cae35ce8a9b0244178bf28e4966c2ce1b8385723a96a6b838858cdd6ca0a1e",
                RoleId = 2
            });

            context.SaveChanges();
        }
    }
}
