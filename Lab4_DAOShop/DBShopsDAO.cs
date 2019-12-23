using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Lab4_DAOShop
{
    public class DBShopsDAO : IShopsDAO
    {
        private readonly SqlConnection _connection;

        private int GetShopID(Shop shop)
        {
            var cmd = new SqlCommand("SELECT ShopID FROM [Shops] " +
                                     "WHERE ShopName = @shopname", _connection);
            cmd.Parameters.AddWithValue("shopname", shop.Name);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return (int)reader[0];
                }
                return -1;
            }
        }

        private int GetProductID(Product product)
        {
            var cmd = new SqlCommand("SELECT ProductID FROM [Products] " +
                                     "WHERE ProductName = @productname", _connection);
            cmd.Parameters.AddWithValue("productname", product.Name);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return (int)reader[0];
                }
                return -1;
            }
        }
        
        public DBShopsDAO()
        {
            _connection = DBUtils.GetSqlConnection();
        }

        public Shop AddShop(string shopName, string shopAddress)
        {
            var cmd = new SqlCommand("INSERT INTO [Shops] " +
                                            "(ShopName, Address) " +
                                            "VALUES(@ShopName, @Address)", _connection);
            cmd.Parameters.AddWithValue("@ShopName", shopName);
            cmd.Parameters.AddWithValue("@Address", shopAddress);
            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
            return new Shop(shopName, shopAddress);
        }

        public Product AddProduct(string productName)
        {
            var cmd = new SqlCommand("INSERT INTO [Products] " +
                                            "(ProductName) " +
                                            "VALUES(@ProductName)", _connection);
            cmd.Parameters.AddWithValue("@ProductName", productName);
            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
            return new Product(productName);
        }
        
        public List<Shop> GetAllShops()
        {
            var cmd = new SqlCommand("SELECT ShopName, ShopAddress FROM [Shops]"
                ,_connection);
            var result = new List<Shop>();
            _connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Shop((string)reader[0], (string)reader[1]));
                }
            }
            _connection.Close();
            return result;
        }

        public List<Product> GetAllProducts()
        {
            var cmd = new SqlCommand("SELECT * FROM [Products]"
                ,_connection);
            
            var result = new List<Product>();
            _connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Product((string)reader[1]));
                }
            }
            _connection.Close();
            return result;
        }
        
        public List<Tuple<Shop, int, double>> GetInfoByProduct(Product product)
        {
            var cmd = new SqlCommand("SELECT ShopName, Quantity, Price FROM [ProductAvailability] JOIN [Shops] " +
                                     "ON ProductAvailability.ShopID = Shops.ShopID " +
                                     "WHERE ProductID IN " +
                                     "(SELECT ProductID FROM [Products] WHERE ProductName = @product)",_connection);
            cmd.Parameters.AddWithValue("@product", product.Name);
            
            var result = new List<Tuple<Shop, int, double>>();
            _connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Tuple<Shop, int, double>(new Shop((string)reader[0]), (int)reader[1], (double)reader[2]));
                }
            }
            _connection.Close();
            return result;
        }

        public Dictionary<Product, Tuple<int, double>> GetInfoByShop(Shop shop)
        {
            var cmd = new SqlCommand("SELECT ProductName, Quantity, Price FROM [ProductAvailability] JOIN [Products] " +
                                     "ON ProductAvailability.ProductID = Products.ProductID " +
                                     "WHERE ShopID IN " +
                                     "(SELECT ShopID FROM [Shops] WHERE ShopName = @shop)",_connection);
            cmd.Parameters.AddWithValue("@shop", shop.Name);
            
            var result = new Dictionary<Product, Tuple<int, double>>();
            _connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result[new Product((string)reader[0])] = new Tuple<int, double>((int)reader[1], (double)reader[2]);
                }
            }
            _connection.Close();
            return result;
        }

        public void AddProductToShop(Shop shop, Product product, int quantity, double price)
        {
            var cmd = new SqlCommand("SELECT Quantity, Price FROM [ProductAvailability] " +
                                     "WHERE ProductID = @productid AND ShopID = @shopid", _connection);
            cmd.Parameters.AddWithValue("@price", price);
            
            _connection.Open();
            cmd.Parameters.AddWithValue("@productid", GetProductID(product));
            cmd.Parameters.AddWithValue("@shopid", GetShopID(shop));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    cmd.CommandText = "UPDATE ProductAvailability " + 
                                      "SET Quantity = @quantity, Price = @price " + 
                                      "WHERE ProductID = @productid AND ShopID = @shopid";
                    cmd.Parameters.AddWithValue("@quantity", (int)reader[0] + quantity);
                }
                else
                {
                    cmd.CommandText = "INSERT INTO [ProductAvailability]" + 
                                      "(ProductID, ShopID, Quantity, Price)" +
                                      "VALUES(@productid, @shopid, @quantity, @price)";
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                }
            }
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public void DeleteProductFromShop(Shop shop, Product product)
        {
            var cmd = new SqlCommand("DELETE FROM [ProductAvailability] " +
                                     "WHERE ProductID = @productID AND ShopID = @shopid");
            cmd.Parameters.AddWithValue("@productid", GetProductID(product));
            cmd.Parameters.AddWithValue("@shopid", GetShopID(shop));
            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public void GetProductsFromShop(Shop shop, Product product, int quantity)
        {
            
            var cmd = new SqlCommand("UPDATE [ProductAvailability] " + 
                                     "SET Quantity = Quantity - @quantity" + 
                                     "WHERE ProductID = @productid AND ShopID = @shopid");
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@productid", GetProductID(product));
            cmd.Parameters.AddWithValue("@shopid", GetShopID(shop));
            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }
}