using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Lab4_DAOShop.Properties
{
    public class FileShopsDAO : IShopsDAO
    {
        private string _shopsFilePath = "/Users/sergei/RiderProjects/sem3_OOPLabs/Lab4_DAOShop/data/shops.csv";
        private string _productsFilePath = "/Users/sergei/RiderProjects/sem3_OOPLabs/Lab4_DAOShop/data/products.csv";
        private int currentShopID = 1;

        private int GetShopID(Shop shop)
        {
            string[] shopLines = File.ReadAllLines(_shopsFilePath);
            var splitShopLines = shopLines.Select(l => l.Split(','));

            int shopId = -1;
            
            foreach (var shopLine in splitShopLines)
            {
                if (shopLine[1] == shop.Name)
                {
                    shopId = Convert.ToInt32(shopLine[0]);
                    break;
                }
            }

            if (shopId == -1)
            {
                throw new ArgumentException();
            }

            return shopId;
        }
        
        
        
        public Shop AddShop(string shopName, string shopAddress)
        {
            using (StreamWriter writer = new StreamWriter(_shopsFilePath, true))
            {
                writer.WriteLine($"{currentShopID},{shopName},{shopAddress}");
                ++currentShopID;
            }
            return new Shop(shopName, shopAddress);
        }
        
        public Product AddProduct(string productName)
        {
            using (StreamWriter writer = new StreamWriter(_productsFilePath, true))
            {
                writer.WriteLine(productName);
            }
            return new Product(productName);
        }

        public FileShopsDAO()
        {
            File.WriteAllText(_shopsFilePath, string.Empty);
            File.WriteAllText(_productsFilePath, string.Empty);
        }
        
        public List<Shop> GetAllShops()
        {
            string[] lines = File.ReadAllLines(_shopsFilePath);
            var splitLines = lines.Select(l => l.Split(','));

            var result = new List<Shop>();
            foreach (var item in splitLines)
            {
                result.Add(new Shop(item[1], item[2]));
            }
            
            return result;
        }

        public List<Product> GetAllProducts()
        {
            string[] lines = File.ReadAllLines(_productsFilePath);
            var splitLines = lines.Select(l => l.Split(','));

            var result = new List<Product>();
            foreach (var item in splitLines)
            {
                result.Add(new Product(item[1]));
            }
            
            return result;
        }
        
        public List<Tuple<Shop, int, double>> GetInfoByProduct(Product product)
        {
            var shops = new Dictionary<int, Shop>();
            string[] lines = File.ReadAllLines(_shopsFilePath);
            var splitLines = lines.Select(l => l.Split(','));

            foreach (var item in splitLines)
            {
                shops[Convert.ToInt32(item[0])] = new Shop(item[1], item[2]);
            }

            lines = File.ReadAllLines(_productsFilePath);
            splitLines = lines.Select(l => l.Split(','));


            List<Tuple<Shop, int, double>> result = new List<Tuple<Shop, int, double>>();
            
            foreach (var item in splitLines)
            {
                if (item[0] == product.Name)
                {
                    for (var i = 1; i < item.Length - 2; i+=3)
                    {
                        result.Add(new Tuple<Shop, int, double>(shops[Convert.ToInt32(item[i])], Convert.ToInt32(item[i+1]), Convert.ToDouble(item[i+2])));
                    }
                }
                break;
            }

            return result;
        }

        public Dictionary<Product, Tuple<int, double>> GetInfoByShop(Shop shop)
        {
            int shopId = GetShopID(shop);
            
            string[] lines = File.ReadAllLines(_productsFilePath);
            var splitLines = lines.Select(l => l.Split(','));
            
            var result = new Dictionary<Product, Tuple<int, double>>();
            
            foreach (var line in splitLines)
            {
                for (var i = 1; i < line.Length - 2; i+=3)
                {
                    if (Convert.ToInt32(line[i]) == shopId)
                    {
                        result[new Product(line[0])] = new Tuple<int, double>(Convert.ToInt32(line[i + 1]), Convert.ToDouble(line[i + 2]));
                        break;
                    }
                }
            }

            return result;
        }
        
        public void AddProductToShop(Shop shop, Product product, int quantity, double price)
        {
            int shopId = GetShopID(shop);

            using (var input = File.OpenText(_productsFilePath))
            using (var output = File.AppendText("swap.tmp"))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    bool found = false;
                    var splitLine = line.Split(',');
                    if (splitLine[0] != product.Name)
                    {
                        output.WriteLine(line);
                        continue;
                    }
                    
                    for (int i = 1; i < splitLine.Length; i+=3)
                    {
                        if (Convert.ToInt32(splitLine[i]) == shopId)
                        {
                            splitLine[i + 1] = Convert.ToString(quantity + Convert.ToInt32(splitLine[i + 1]));
                            splitLine[i + 2] = Convert.ToString(price);
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        splitLine[0]+=($",{shopId.ToString()},{quantity.ToString()},{price.ToString()}");
                    }

                    for (int i = 0; i < splitLine.Length - 1; ++i)
                    {
                        output.Write(splitLine[i] + ',');
                    }
                    output.WriteLine(splitLine[splitLine.Length - 1]);
                }
            }

            File.Delete(_productsFilePath);
            File.Move("swap.tmp", _productsFilePath);
        }

        public void GetProductsFromShop(Shop shop, Product product, int quantity)
        {
            int shopId = GetShopID(shop);
            
            using (var input = File.OpenText(_productsFilePath))
            using (var output = File.AppendText("swap.tmp"))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    var splitLine = line.Split(',');
                    if (splitLine[0] != product.Name)
                    {
                        output.WriteLine(line);
                        continue;
                    }
                    
                    for (int i = 1; i < splitLine.Length; i+=3)
                    {
                        if (Convert.ToInt32(splitLine[i]) == shopId)
                        {
                            splitLine[i + 1] = Convert.ToString(Convert.ToInt32(splitLine[i + 1]) - quantity);
                            break;
                        }
                    }


                    for (int i = 0; i < splitLine.Length - 1; ++i)
                    {
                        output.Write(splitLine[i] + ',');
                    }
                    output.WriteLine(splitLine[splitLine.Length - 1]);
                }
            }

            File.Delete(_productsFilePath);
            File.Move("swap.tmp", _productsFilePath);
        }
        
        public void DeleteProductFromShop(Shop shop, Product product)
        {
            throw new NotImplementedException();
        }
    }
}