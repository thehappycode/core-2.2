using System;
using System.Collections.Generic;
using System.Text;

namespace BOS.BUSINESS.Helper
{
    public class ApplicationSetting
    {
        public Jwt Jwt { get; set; }
        public string BaseUrlApiGateWay { get; set; }
    }
    public class Jwt
    {
        public string Sub { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
