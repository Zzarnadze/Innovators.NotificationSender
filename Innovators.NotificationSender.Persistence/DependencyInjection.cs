using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Innovators.NotificationSender.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer();
            services.AddEntityFrameworkProxies();

            services.AddDbContext<NotificationSenderDbCondetxt>((serviceProvider, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("NotificationSenderConnectionString"));
                options.UseLazyLoadingProxies();
                options.UseInternalServiceProvider(serviceProvider);
            });

            services.AddScoped<INotificationSenderDbcontext>(provider => provider.GetService<NotificationSenderDbCondetxt>());

            return services;

        }

    }
    }
