using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Test.Core.Model
{
    public class User
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TimeStamp { get; set; }
        public static User ConvertToModel(DataRow row)
        {
            var model = new User();
            model.UserID = row.Table.Columns.Contains("UserID") ? Convert.ToString(row["UserID"]) : "";
            model.UserName = row.Table.Columns.Contains("UserName") ? Convert.ToString(row["UserName"]) : "";
            model.FirstName = row.Table.Columns.Contains("FirstName") ? Convert.ToString(row["FirstName"]) : "";
            model.LastName = row.Table.Columns.Contains("LastName") ? Convert.ToString(row["LastName"]) : "";
            model.TimeStamp = row.Table.Columns.Contains("TimeStamp") && !string.IsNullOrEmpty(row["TimeStamp"].ToString()) ?
                Convert.ToDateTime(row["TimeStamp"]).ToString("dd-MMM-yyyy") : "";
            return model;

        }
    }
}