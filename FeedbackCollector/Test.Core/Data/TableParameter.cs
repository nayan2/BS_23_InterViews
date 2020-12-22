using System.Data;

namespace Test.Core.Data
{
    public class TableParameter
    {
        public string TableName { get; set; }
        public DataTable Table { get; set; }
        public string TypeName { get; set; }
        public SqlDbType SqlDbType { get; set; }
    }
}