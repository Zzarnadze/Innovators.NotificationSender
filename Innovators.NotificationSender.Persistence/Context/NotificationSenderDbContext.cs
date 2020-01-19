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
    /// <summary>
    /// Db Context
    /// </summary>
    public class NotificationSenderDbContext : DbContext, INotificationSenderDbContext
    {
        /// <summary>
        /// Constructor for NotificationSenderDbContext
        /// </summary>
        /// <param name="options">Db context options</param>
        public NotificationSenderDbContext(DbContextOptions<NotificationSenderDbContext> options)
            : base(options) { }

        /// <summary>
        /// Triggers on model creating
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationSenderDbContext).Assembly);
        }

        #region DbSets
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<MailSetting> MailSettings { get; set; }
        public DbSet<SmsSetting> SmsSettings { get; set; }
        public DbSet<MailTemplate> MailTemplates { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Saves changes in the database
        /// </summary>
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        /// <summary>
        /// Saves changes in the database asynchronously
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds time stamps to the entity (Creation date or last modification date)
        /// Also sets created by user id or last modified by customer id, which is provided by identity provider todo: this
        /// </summary>
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
        #endregion

    }
}
