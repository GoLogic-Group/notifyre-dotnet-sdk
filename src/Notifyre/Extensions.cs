using System;
using Microsoft.Extensions.DependencyInjection;
using Notifyre.Services.Fax.FaxReceive;
using Notifyre.Services.Fax.FaxSend;

namespace Notifyre
{
    public static class Extensions
    {
        public static void AddNotifyre(this IServiceCollection services, Action<NotifyreConfiguration> configure)
        {
            var config = new NotifyreConfiguration();
            configure(config);
            services.AddSingleton(config);
            services.AddTransient<FaxSendService>();
            services.AddTransient<FaxReceiveService>();
            services.AddTransient<FaxService>();
            services.AddTransient<SmsService>();
            services.AddTransient<ContactsService>();
        }
    }
}
