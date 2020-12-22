using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Test.Core.Model
{
    public class Activity
    {
        public string CommentID { get; set; }
        public string UserID { get; set; }
        public string ActivityID { get; set; }
        public string ActivityType { get; set; }
        public string TimeStamp { get; set; }
        public static Activity ConvertToModel(DataRow row)
        {
            var model = new Activity();
            model.CommentID = row.Table.Columns.Contains("CommentID") ? Convert.ToString(row["CommentID"]) : "";
            model.UserID = row.Table.Columns.Contains("UserID") ? Convert.ToString(row["UserID"]) : "";
            model.ActivityID = row.Table.Columns.Contains("ActivityID") ? Convert.ToString(row["ActivityID"]) : "";
            model.ActivityType = row.Table.Columns.Contains("ActivityType") ? Convert.ToString(row["ActivityType"]) : "";
            model.TimeStamp = row.Table.Columns.Contains("TimeStamp") && !string.IsNullOrEmpty(row["TimeStamp"].ToString()) ?
                Convert.ToDateTime(row["TimeStamp"]).ToString("dd-MMM-yyyy") : "";
            return model;

        }
    }
}