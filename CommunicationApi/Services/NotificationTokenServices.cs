namespace CommunicationApi.Services
{
    public sealed class NotificationTokenServices
    {

        private static Dictionary<string, string> firebaseTokens = null;

        private NotificationTokenServices() { }


        public static Dictionary<String, String> FirebaseTokens
        {
            get
            {
                if (firebaseTokens == null)
                {
                    firebaseTokens = new Dictionary<string, string>();
                }
                return firebaseTokens;
            }
        }

  
    }
}
