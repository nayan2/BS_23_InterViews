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
    public class UserManager : BllCommon, IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager()
        {
            userRepository = new UserRepository(_dbContext);
        }

        public User Get(string userName, string password)
        {
            try
            {
                _dbContext.Open();
                return userRepository.Get(userName, password);
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