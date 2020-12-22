using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Test.Core.Model;

namespace Test.Core.Bll
{
    public class BllCommon
    {
        public Data.DbContext _dbContext;
        public Message _vmReturn;
        public SqlTransaction Transaction { get; set; }

        public BllCommon()
        {
            _vmReturn = new Message();
            _dbContext = new Data.DbContext(AppDbConnection.ConnectionString, "System.Data.SqlClient");
        }
    }
}