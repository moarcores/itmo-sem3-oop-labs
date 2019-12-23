using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Lab4_DAOShop.Properties;

namespace Lab4_DAOShop
{
    public class ShopsService
    {
        private readonly IShopsDAO _shopsDao;

        public ShopsService()
        {
            if (ConfigurationManager.AppSettings["DAO"] == "MSSQL")
            {
                _shopsDao = new DBShopsDAO();
                DBUtils.ClearData();
            }
            else
            {
                _shopsDao = new FileShopsDAO();
            }
        }
        
        public Shop AddShop(string shopName, string shopAddress)
        {
            return _shopsDao.AddShop(shopName, shopAddress);
        }

        public Product AddProduct(string productName)
        {
            return _shopsDao.AddProduct(productName);
        }

        public void AddProductToShop(Product product, Shop shop, int quantity, double price)
        {
            _shopsDao.AddProductToShop(shop, product, quantity, price);
        }
        
        public Shop FindCheapest(Product product)
        {
            var collection = _shopsDao.GetInfoByProduct(product);
            Shop result = null;
            var min = double.MaxValue;
            
            foreach (var (item1, _, item3) in collection.Where(item => item.Item3 < min))
            {
                min = item3;
                result = item1;
            }
            return result;
        }
        
        public List<Tuple<Product, int>> QuantityByPrice(Shop shop, double sum)
        {
            var collection = _shopsDao.GetInfoByShop(shop);
            if (collection.Count == 0)
            {
                return null;
            }
            int quantity;
            var result = new List<Tuple<Product, int>>();
            foreach (var item in collection)
            {
                quantity = (int)Math.Floor(sum / item.Value.Item2);
                result.Add(new Tuple<Product, int>(item.Key, Math.Min(quantity, item.Value.Item1)));
            }
            return result;
        }
        
        public double MakePurchase(Shop shop, Dictionary<Product, int> order)
        {
            double result = CheckPurchase(shop, order);
            if (result > 0)
            {
                CommitPurchase(shop, order);
            }

            return result;
        }

        public Shop FindCheapestOrder(Dictionary<Product, int> order)
        {
            double min = double.MaxValue;
            Shop result = null;
            foreach (var shop in GetAllShops())
            {
                double tmp = CheckPurchase(shop, order);
                if (tmp > 0 && tmp < min)
                {
                    min = tmp;
                    result = shop;
                }
            }

            return result;
        }
        
        private double CheckPurchase(Shop shop, Dictionary<Product, int> order)
        {
            var collection = _shopsDao.GetInfoByShop(shop);
            double result = 0;    
            foreach (var item in order)
            {
                if (collection.ContainsKey(item.Key) && collection[item.Key].Item1 >= item.Value)
                {
                    result += item.Value * collection[item.Key].Item2;
                }
                else
                {
                    return -1;
                }
            }

            return result;
        }

        private void CommitPurchase(Shop shop, Dictionary<Product, int> order)
        {
            foreach (var item in order)
            {
                _shopsDao.GetProductsFromShop(shop, item.Key, item.Value);
            }
        }

        public void InfoByShop(Shop shop)
        {
            foreach (var keyValuePair in _shopsDao.GetInfoByShop(shop))
            {
                Console.Out.WriteLine(keyValuePair.Key + " " + keyValuePair.Value.Item1 + " " + keyValuePair.Value.Item2);
            }
        }
        
        public void InfoByProduct(Product product)
        {
            _shopsDao.GetInfoByProduct(product).ForEach(Console.Out.WriteLine);
        }
        
        public List<Shop> GetAllShops()
        {
            return _shopsDao.GetAllShops();
        }
        
        public List<Product> GetAllProducts()
        {
            return _shopsDao.GetAllProducts();
        }
    }
}