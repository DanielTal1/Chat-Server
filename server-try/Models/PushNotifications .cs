using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace server_try.Models
{
    public class PushNotifications
    {
        private static FirebaseMessaging messaging;
        public PushNotifications()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                var app = FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile("private_key.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
                messaging = FirebaseMessaging.GetMessaging(app);
            }
        }

        private Message CreateNotification(string title, string notificationBody, string token)
        {
            return new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title
                }
            };
        }

        public async Task SendNotification(string token, string title, string body)
        {
            var result = await messaging.SendAsync(CreateNotification(title, body, token));
            //do something with result
        }
    }
}
