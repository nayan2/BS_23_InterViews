using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Core.Bll.Interfaces;
using Test.Core.Data.Interfaces;
using Test.Core.Data.Repositories;
using Test.Core.Model;

namespace Test.Core.Bll.Managers
{
    public class ActivityManager : BllCommon, IActivityManager
    {
        private readonly IActivityRepository activityRepository;

        public ActivityManager()
        {
            activityRepository = new ActivityRepository(_dbContext);
        }


        public int ToggleCommentActivity(Activity activity)
        {
            try
            {
                _dbContext.Open();
                var affectedRow = activityRepository.ToggleCommentActivity(activity);
                return affectedRow;
            }
            catch (Exception ex)
            {
                throw new Exception("Data retrival failed.");
            }
            finally
            {
                _dbContext.Close();
            }

        }
    }
}