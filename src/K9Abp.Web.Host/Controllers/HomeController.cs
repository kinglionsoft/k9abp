using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Timing;
using K9Abp.Web.Core.Controllers;
using System.Diagnostics;
using Abp.Auditing;
using Abp.Domain.Repositories;

namespace K9Abp.Web.Host.Controllers
{
    public class HomeController : K9AbpControllerBase
    {
        private readonly INotificationPublisher _notificationPublisher;

        public HomeController(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        public IActionResult Index()
        {
            return Redirect("/swagger");
        }

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                    "App.SimpleMessage",
                    new MessageNotificationData(message),
                    severity: NotificationSeverity.Info,
                    userIds: new[] { defaultTenantAdmin, hostAdmin }
                 );

            return Content("Sent notification: " + message);
        }

        [DisableAuditing]
        public async Task<long> Test(int c, [FromServices]IBulkRepository<Demo> repository)
        {
            var data = Enumerable.Range(1, c)
                .Select( x => new Demo());
            var sw = new Stopwatch();
            sw.Start();
            await repository.BulkInsertAsync(data);
            sw.Stop();
            return  sw.ElapsedMilliseconds;
        }
    }

    [DisableAuditing]
    public class Demo : Abp.Domain.Entities.Entity
    {
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public decimal Fee { get; set; }

        public int Status { get; set; }

        public Demo()
        {
            Name = "Demo";
            CreateTime = DateTime.Now;
            Fee = 100M;
            Status = 1;
        }
    }
}

