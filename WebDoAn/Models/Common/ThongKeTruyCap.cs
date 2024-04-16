using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using WebDoAn.Models;

namespace WebDoAn.Models.Common
{
    public class ThongKeTruyCap
    {

        public static string strConnect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public static ThongKeViewModel ThongKe()
        {
            using (var connect = new SqlConnection(strConnect))
            {
                var item = connect.QueryFirstOrDefault<ThongKeViewModel>("tk_ThongKe", commandType: CommandType.StoredProcedure);
                return item;
            }
        }
    }
}