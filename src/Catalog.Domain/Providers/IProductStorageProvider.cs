using Catalog.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Business.Providers
{
    public interface IProductStorageProvider
    {
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Guid id);
        Product GetProductById(Guid id);
        List<Product> GetAllProducts();
    }
}
