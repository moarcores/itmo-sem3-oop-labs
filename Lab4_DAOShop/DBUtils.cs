using System.Data.SqlClient;
//sqlcmd -S localhost -U SA -P "S70127012s"

namespace Lab4_DAOShop
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
        
        public static void ClearData()
        {
            var connection = DBUtils.GetSqlConnection();
            connection.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM ProductAvailability", connection);
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = "DELETE FROM Shops";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DBCC CHECKIDENT ('Shops', RESEED, 0)";
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = "DELETE FROM Products";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DBCC CHECKIDENT ('Products', RESEED, 0)";
            cmd.ExecuteNonQuery();
            
            connection.Close();
        }
    }
}