using StoreApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Models;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        

        // GET: api/Products
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public IEnumerable<Product> GetProducts()
        {
            return _productsRepository.GetAll();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await _productsRepository.Find(id);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

           await _productsRepository.Update(product);

           

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProducts([FromBody] Product products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productsRepository.Add(products);

            return CreatedAtAction("GetProducts", new { id = products.ProductId }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await _productsRepository.Find(id);
            if (products == null)
            {
                return NotFound();
            }

            await _productsRepository.Remove(id);

            return Ok(products);
        }

        private async Task<bool> ProductsExists(int id)
        {
            return await _productsRepository.IsExists(id);
        }
    }
}