using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Test.Core.Data.Interfaces;
using Test.Core.Model;

namespace Test.Core.Data.Repositories
{
    public class PostRepository : DataCommon, IPostRepository
    {
        public PostRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Post> GetAllPosts(int pageNo, string searcText, int entries)
        {
            var query = "exec dbo.GetAllPostInfo @PageNo,@Searchtext,@entries";
            _inputParameters.Clear();
            _inputParameters.Add(new Parameter { Name = "@PageNo", Type = DbType.String, Value = pageNo });
            _inputParameters.Add(new Parameter
            {
                Name = "@Searchtext",
                Type = DbType.String,
                Value = string.IsNullOrEmpty(searcText) ?
               "" : searcText
            });
            _inputParameters.Add(new Parameter { Name = "@entries", Type = DbType.String, Value = entries });

            var dt = _dbContext.GetDataTable(query, _inputParameters);
            if (dt.Rows.Count > 0)
                return from DataRow row in dt.Rows
                       let CommentList = GetComments(Convert.ToString(row["PostID"]))
                       select Post.ConvertToModel(row, CommentList);
            return null;
        }

        public IEnumerable<Comment> GetComments(string postId)
        {
            var query = @"select b.CommentID,b.Text CommentText,
             d.FirstName+' '+d.LastName CommentBy, b.TimeStamp CommentTime , 
             Likes=(select count(*) from Activity x inner join Comment y on x.CommentID=y.CommentID where x.CommentID=b.CommentID and x.ActivityType='Like'),
             Dislikes=(select count(*) from Activity x inner join Comment y on x.CommentID=y.CommentID where x.CommentID=b.CommentID and x.ActivityType='Dislike')
             from Post a 
             inner join Comment b on a.PostID=b.PostID
             inner join [User] d on d.userID=b.UserID
             where b.PostID=@PostID";
            _inputParameters.Clear();
            _inputParameters.Add(new Parameter { Name = "@PostID", Type = DbType.String, Value = postId });

            var dt = _dbContext.GetDataTable(query, _inputParameters);
            if (dt.Rows.Count > 0)
                return from DataRow row in dt.Rows select Comment.ConvertToModel(row);
            return null;
        }

       
    }
}