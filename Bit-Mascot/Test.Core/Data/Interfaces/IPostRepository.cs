using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Model;

namespace Test.Core.Data.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts(int pageNo, string searcText, int entries);
        
    }
}
