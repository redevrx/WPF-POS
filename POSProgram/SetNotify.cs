using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProgram
{
    class SetNotify
    {
        private string title;
        private string message;


        public SetNotify() { }
        public SetNotify(string title, string message)
        {
            this.title = title;
            this.message = message;
        }

        public string Title { get => title; set => title = value; }
        public string Message { get => message; set => message = value; }

        public void ShowNotfySucess()
        {
            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Success
            }, expirationTime: TimeSpan.FromSeconds(10), onClose: () => Console.Write("close"));
        }

        public void ShowNotfyInformation()
        {
            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Information
            }, expirationTime: TimeSpan.FromSeconds(10), onClose: () => Console.Write("close"));
        }

        public void ShowNotfyWarning(string title, string message)
        {
            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Warning
            }, expirationTime: TimeSpan.FromSeconds(10), onClose: () => Console.Write("close"));
        }

        public void ShowNotfyError()
        {
            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Error
            }, expirationTime: TimeSpan.FromSeconds(10),onClose:()=>Console.Write("close"));
        }
    }
}
