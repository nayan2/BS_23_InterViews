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
    public class PostManager : BllCommon, IPostManager
    {
        private readonly IPostRepository PostRepository;

        public PostManager()
        {
            PostRepository = new PostRepository(_dbContext);
        }

        public IEnumerable<Post> GetAllPosts(int pageNo, string searcText, int entries)
        {
            try
            {
                _dbContext.Open();
                return PostRepository.GetAllPosts( pageNo,  searcText,  entries);
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