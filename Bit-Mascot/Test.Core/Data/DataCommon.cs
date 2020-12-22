using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Core.Data
{
    public class DataCommon
    {
        public DbContext _dbContext;
        public List<Parameter> _inputParameters;
        public List<Parameter> _outputParameters;
        public List<TableParameter> _tableParameters;
        public string _query;
        public Boolean _isSuccess;
        public int _affectedRow;
        public DataCommon()
        {
            _inputParameters = new List<Parameter>();
            _outputParameters = new List<Parameter>();
            _tableParameters = new List<TableParameter>();
        }
    }
}