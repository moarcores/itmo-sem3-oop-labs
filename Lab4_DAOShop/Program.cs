using System;
using System.Collections.Generic;

namespace Lab4_DAOShop
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ShopsService service = new ShopsService();
            
            var shop = service.AddShop("Diksi", "Tam");
            var shop1 = service.AddShop("5orOchka", "Na meste");
            var product = service.AddProduct("Winston Blue");
            var product1 = service.AddProduct("Limonad");
            
            
            service.AddProductToShop(product1, shop, 10, 10);
            service.AddProductToShop(product, shop, 5, 10);


            var order = new Dictionary<Product, int>();
            order[product1] = 2;
            order[product] = 1;


            service.MakePurchase(shop, order);
            
            service.InfoByShop(shop);
            service.InfoByProduct(product1);


            service.GetAllShops();
        }
    }
}