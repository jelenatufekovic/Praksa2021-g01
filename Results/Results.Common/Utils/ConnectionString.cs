using System.Configuration;

namespace Results.Common.Utils
{
    public class ConnectionString
    {
        public static string GetDefaultConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ResultsDb"].ConnectionString;
        }
    }
}
