using Catalog.Business.Model;
using Catalog.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        public ProductService ProductService { get; }

        public CatalogController(ProductService productService) 
        {
            ProductService = productService;
        }

        // GET: api/<CatalogController>
        [HttpGet]
        public List<Product> Get()
        {
            return ProductService.GetAll();
        }

        // GET api/<CatalogController>/5
        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            return ProductService.Get(id);
        }

        // POST api/<CatalogController>
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            ProductService.Add(product);
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Product product)
        {
            ProductService.Update(product);
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            ProductService.Delete(id);
        }
    }
}
