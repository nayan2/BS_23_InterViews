using System.Data;

namespace Test.Core.Data
{
    public class Parameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public DbType Type { get; set; }
        public SqlDbType SqlDbType { get; set; }
    }
}