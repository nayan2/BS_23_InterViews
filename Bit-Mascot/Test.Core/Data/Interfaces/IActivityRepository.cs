﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Model;

namespace Test.Core.Data.Interfaces
{
    public interface IActivityRepository
    {
        int ToggleCommentActivity(Activity activity);
    }
}
