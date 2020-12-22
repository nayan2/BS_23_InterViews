using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Test.Core.Model
{
    public class Comment
    {
        public string CommentID { get; set; }
        public string PostID { get; set; }
        public string Text { get; set; }
        public string TimeStamp { get; set; }
        public int Likes { get; set; }
        public string CommentBy { get; set; }

        public int Dislikes { get; set; }
        public string CommentTime { get; set; }
        public string CommentText { get; set; }
        public static Comment ConvertToModel(DataRow row)
        {
            var model = new Comment();
            model.CommentID = row.Table.Columns.Contains("CommentID") ? Convert.ToString(row["CommentID"]) : "";
            model.PostID = row.Table.Columns.Contains("PostID") ? Convert.ToString(row["PostID"]) : "";
            model.Text = row.Table.Columns.Contains("Text") ? Convert.ToString(row["Text"]) : "";
            model.TimeStamp = row.Table.Columns.Contains("TimeStamp") && !string.IsNullOrEmpty(row["TimeStamp"].ToString()) ?
                Convert.ToDateTime(row["TimeStamp"]).ToString("dd-MMM-yyyy") : "";
            model.CommentText = row.Table.Columns.Contains("CommentText") ? Convert.ToString(row["CommentText"]) : "";
            model.CommentBy = row.Table.Columns.Contains("CommentBy") ? Convert.ToString(row["CommentBy"]) : "";
            model.Likes = row.Table.Columns.Contains("Likes") ? Convert.ToInt32(row["Likes"]) : 0;
            model.Dislikes = row.Table.Columns.Contains("Dislikes") ? Convert.ToInt32(row["Dislikes"]) : 0;
            model.CommentTime = row.Table.Columns.Contains("CommentTime") && !string.IsNullOrEmpty(row["CommentTime"].ToString()) ?
              Convert.ToDateTime(row["CommentTime"]).ToString("dd-MMM-yyyy") : "";
            return model;

        }
    }
}