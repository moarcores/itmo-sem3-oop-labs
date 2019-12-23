using System;
using System.Collections.Generic;

namespace Lab4_DAOShop
{
    public interface IShopsDAO
    {
        Shop AddShop(string shopName, string shopAddress);
        
        Product AddProduct(string productName);
        
        List<Shop> GetAllShops();
        
        List<Product> GetAllProducts();
        
        List<Tuple<Shop, int, double>> GetInfoByProduct(Product product);

        Dictionary<Product, Tuple<int, double>> GetInfoByShop(Shop shop);

        void AddProductToShop(Shop shop, Product product, int quantity, double price);

        void GetProductsFromShop(Shop shop, Product product, int quantity);

        void DeleteProductFromShop(Shop shop, Product product);
    }
}