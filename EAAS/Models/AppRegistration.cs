﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAAS.Models
{
    public class AppRegistration
    {
        public string AppId { get; set; }
        public string UserId { get; set; }
        public string AppName { get; set; }
        public List<string> Urls { get; set; }
        public Dictionary<string,object> @AppEncryptionKey { get; set; }
    }

    public class AppDetails
    {
       public List<UserAppinfo> AppInfo { get; set; }
    }
    public class UserAppinfo
    {
        public string UserId { get; set; }
        public string AppName { get; set; }       
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
    }



}