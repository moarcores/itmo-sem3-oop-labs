using System.Data.SqlClient;

namespace Lab5_Serialization
{
    public class DBUtils
    {
        public static SqlConnection GetSqlConnection()
        {
            const string datasource = "localhost";
            const string database = "Lab4_ShopDB";
            const string username = "SA";
            const string password = "S70127012s";

            return new SqlConnection(
                $@"Data Source={datasource};Initial Catalog={database};User ID={username};Password={password}");
        }
    }
}