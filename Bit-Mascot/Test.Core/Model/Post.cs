using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Test.Core.Model
{
    public class Post
    {
        public string PostID { get; set; }
        public string Text { get; set; }
        public string UserID { get; set; }

        public string PostBy { get; set; }
        public string TimeStamp { get; set; }

        public string PostTime { get; set; }
        public int TotalInfo { get; set; }
        public int Comments { get; set; }
        public List<Comment> CommentList { get; set; }
        public static Post ConvertToModel(DataRow row, IEnumerable<Comment> CommentList=null)
        {
            var model = new Post();
            model.PostID = row.Table.Columns.Contains("PostID") ? Convert.ToString(row["PostID"]) : "";
            model.Text = row.Table.Columns.Contains("Text") ? Convert.ToString(row["Text"]) : "";
            model.UserID = row.Table.Columns.Contains("UserID") ? Convert.ToString(row["UserID"]) : "";
            model.PostBy = row.Table.Columns.Contains("PostBy") ? Convert.ToString(row["PostBy"]) : "";
            model.TotalInfo = row.Table.Columns.Contains("TotalInfo") ? Convert.ToInt32(row["TotalInfo"]) : 0;
            model.Comments = row.Table.Columns.Contains("Comments") ? Convert.ToInt32(row["Comments"]) : 0;
            model.TimeStamp = row.Table.Columns.Contains("TimeStamp") && !string.IsNullOrEmpty(row["TimeStamp"].ToString()) ?
                Convert.ToDateTime(row["TimeStamp"]).ToString("dd-MMM-yyyy") : "";

            model.PostTime = row.Table.Columns.Contains("PostTime") && !string.IsNullOrEmpty(row["PostTime"].ToString()) ?
                Convert.ToDateTime(row["PostTime"]).ToString("dd-MMM-yyyy") : "";

            if (CommentList != null)
            {
                model.CommentList = CommentList.ToList();
            }
            else
            {
                model.CommentList = null;
            }
            return model;

        }
    }
}