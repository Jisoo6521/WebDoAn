using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using WebDoAn.Models;

namespace WebDoAn.Common
{
    public class SettingHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        //public static string GetValue(string key)
        //{
        //    var item = db.SystemSettings.SingleOrDefault(x => x.SettingKey == key);
        //    if (item != null)
        //    {
        //        return item.SettingValue;
        //    }
        //    return "";
        //}
        public static string GetValue(string key)
        {
            if (db?.SystemSettings != null)
            {
                var item = db.SystemSettings.SingleOrDefault(x => x.SettingKey == key);
                if (item != null)
                {
                    return item.SettingValue;
                }
            }
            return "";
        }
    }
}