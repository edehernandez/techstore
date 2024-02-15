using Catalog.Business.Model;
using Catalog.Business.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Business.Services
{
    public class ProductService
    {
        public IProductStorageProvider ProductStorage { get; }

        public ProductService(IProductStorageProvider productStorage)
        {
            ProductStorage = productStorage;
        }

        public void Add(Product product)
        {
            ProductStorage.AddProduct(product);
        }

        public void Update(Product product)
        {
            ProductStorage.UpdateProduct(product);
        }

        public void Delete(Guid id)
        {
            ProductStorage.DeleteProduct(id);
        }

        public Product Get(Guid id)
        {
            return ProductStorage.GetProductById(id);
        }

        public List<Product> GetAll()
        {
            return ProductStorage.GetAllProducts();
        }
    }
}
