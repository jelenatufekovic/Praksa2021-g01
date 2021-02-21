using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common
{
    public static class TokenSettings
    {
        private static string _issuer;
        private static string _audience;
        private static string _secret;

        public static string Issuer
        {
            get 
            {
                if (_issuer == null)
                {
                    _issuer = ConfigurationManager.AppSettings.Get("Issuer").ToString();
                }
                return _issuer;
            }
            set
            {
                _issuer = value;
            }
        }

        public static string Audience
        {
            get
            {
                if (_audience == null)
                {
                    _audience = ConfigurationManager.AppSettings.Get("Audience").ToString();
                }
                return _audience;
            }
            set
            {
                _audience = value;
            }
        }

        public static string Secret
        {
            get
            {
                if (_secret == null)
                {
                    _secret = ConfigurationManager.AppSettings.Get("Secret").ToString();
                }
                return _secret;
            }
            set
            {
                _secret = value;
            }
        }
    }
}
