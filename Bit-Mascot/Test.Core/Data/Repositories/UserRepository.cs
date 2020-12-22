using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Test.Core.Data.Interfaces;
using Test.Core.Model;

namespace Test.Core.Data.Repositories
{
    public class UserRepository : DataCommon, IUserRepository
    {
        public UserRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Get(string userName,string password)
        {
            var query = "Select * from [User] u where u.UserName=@UserName and u.Password=@Password";
            _inputParameters.Clear();
            _inputParameters.Add(new Parameter { Name = "@UserName", Type = DbType.String, Value = userName });
            _inputParameters.Add(new Parameter { Name = "@Password", Type = DbType.String, Value = password });

            var dt = _dbContext.GetDataTable(query, _inputParameters);
            if (dt.Rows.Count > 0)
                return User.ConvertToModel(dt.Rows[0]);
            return null;
        }
    }
}