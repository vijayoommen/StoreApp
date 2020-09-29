using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using StoreApp.UI.Application;
using StoreApp.UI.Domain;
using System.Linq;
using Newtonsoft.Json;

namespace StoreApp.UI.DataAccess
{
    public interface IProductStore
    {
        Product GetProductbyId(int id);
        Product GetProductBySku(string sku);
        void SaveProduct(Product p);
    }

    public class ProductStore : IProductStore
    {
        private readonly IAppConfiguration _configuration;
        private readonly IFileHandler fileHander;

        public ProductStore(IAppConfiguration configuration, IFileHandler fileHander)
        {
            _configuration = configuration;
            this.fileHander = fileHander;
        }

        public Product GetProductbyId(int id)
        {
            return GetProducts().Where(x => x.Id == id).FirstOrDefault();
        }

        public Product GetProductBySku(string sku)
        {
            return GetProducts().Where(x => x.Sku == sku).FirstOrDefault();
        }

        public void SaveProduct(Product p)
        {
            var products = GetProducts();
            var indx = products.FindIndex(x => x.Id == p.Id);

            if (indx < 0)
                products.Add(p);
            else
                products[indx] = p;

            var content = JsonConvert.SerializeObject(products, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            fileHander.Writeall(_configuration.ProductStore, content);
        }

        private List<Product> GetProducts()
        {
            var contents = fileHander.ReadAll(_configuration.ProductStore);
            var products = JsonConvert.DeserializeObject<List<Product>>(contents, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            return products;
        }
    }
}
