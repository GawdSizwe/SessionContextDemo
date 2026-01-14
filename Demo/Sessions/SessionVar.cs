using Demo.Models;

namespace Demo.Sessions
{
    public class SessionVar : SessionManager
    {
        public static string FirstName 
        { 
            get => Get<string>("FirstName");
            set => Set("FirstName", value); 
        }

        public static UserData UserDataValues
        {
            get => Get<UserData>("UserDataValues");
            set => Set("UserDataValues", value);
        }

    }
}
