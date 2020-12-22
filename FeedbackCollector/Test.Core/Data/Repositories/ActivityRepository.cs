using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Test.Core.Data.Interfaces;
using Test.Core.Model;

namespace Test.Core.Data.Repositories
{
    public class ActivityRepository : DataCommon, IActivityRepository
    {
        public ActivityRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int ToggleCommentActivity(Activity activity)
        {
            var query = @"if exists( select * from Activity a where a.CommentID=@CommentID  and a.UserID=@UserID)
                         begin
                         update a set a.ActivityType = @ActivityType from Activity a where a.CommentID = @CommentID  and a.UserID = @UserID
                         end
                         else
                         begin
                         insert into Activity (CommentID, ActivityType, UserID, TimeStamp)values(@CommentID, @ActivityType, @UserID, GETDATE())
                         end";
            _inputParameters.Clear();
            _inputParameters.Add(new Parameter { Name = "@ActivityType", Type = DbType.String, Value = activity.ActivityType });
            _inputParameters.Add(new Parameter { Name = "@CommentID", Type = DbType.String, Value = activity.CommentID });
            _inputParameters.Add(new Parameter { Name = "@UserID", Type = DbType.String, Value = activity.UserID });

            var res = _dbContext.ExecuteQuery(query, _inputParameters);
            return res;
        }
    }
}