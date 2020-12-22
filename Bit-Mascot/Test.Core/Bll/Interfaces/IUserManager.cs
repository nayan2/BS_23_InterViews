using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Model;

namespace Test.Core.Bll.Interfaces
{
    public interface IUserManager
    {
        User Get(string userName, string password);
    }
}
