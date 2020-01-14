using Microsoft.EntityFrameworkCore;
using Innovators.NotificationSender.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Innovators.NotificationSender.Persistence.Context
{
    public class NotificationSenderDbCondetxt :DbContext, INotificationSenderDbcontext
    {
        public NotificationSenderDbCondetxt(DbContextOptions<NotificationSenderDbCondetxt> options)
            : base(options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationSenderDbCondetxt).Assembly);
        }
       public  DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MailConfiguration> MailConfigurations { get; set; }
        public DbSet<MailTemplate> MailTemplates { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                    ((Base)entity.Entity).CreationDate = DateTime.Now;
                else
                    ((Base)entity.Entity).LastModificationDate = DateTime.Now;
            }
        }

    }
}
