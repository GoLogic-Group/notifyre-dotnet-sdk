using System.Reflection;

namespace Notifyre
{
    public class NotifyreConfiguration
    {
        public string NotifyreNetVersion { get; }
        public string ApiToken { get; set; }


        public NotifyreConfiguration()
        {
            NotifyreNetVersion = new AssemblyName(typeof(NotifyreConfiguration).GetTypeInfo().Assembly.FullName).Version.ToString(3);
        }
    }
}
