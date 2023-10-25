using Microsoft.AspNetCore.Mvc;
using Storage.Models;

namespace Storage.Controllers
{
    [Route("/api/[controller]")]
    public class ProductsController : Controller
    {
        private static List<Product> products = new List<Product>(new[]
        {
            new Product(){ID = 1, Name ="First prod", Price = 100, Quantity = 1, Description = "First prod descr"},
            new Product(){ID = 2, Name ="Second prod", Price = 200, Quantity = 2, Description = "Second prod descr"},
            new Product(){ID = 3, Name ="Third prod", Price = 300, Quantity = 3, Description = "Third prod descr"}
        });

        [HttpGet]
        public IEnumerable<Product> Get() => products;

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = products.SingleOrDefault(p => p.ID == id);
            
            if (product == null)
                return NotFound();
            
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            products.Remove(products.SingleOrDefault(p => p.ID == id));
            return Ok(new {Message = "Deleted successfully"});
        }

        private int NextProductID => products.Count() == 0 ? 1 : products.Max(x => x.ID) + 1;
        [HttpGet("GetNextProductID")]
        public int GetNextProductID()
        {
            return NextProductID;
        }

        [HttpPost]
        public IActionResult Post(Product product) // если есть заполненная web-форма c required-параметрами
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            product.ID = NextProductID;
            products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.ID }, product);
        }
        [HttpPost("AddProduct")]
        public IActionResult PostBody([FromBody] Product product) => Post(product);

        [HttpPut]
        public IActionResult Put(Product product)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var storedProduct = products.SingleOrDefault(p => p.ID == product.ID);
            if (storedProduct == null)
                return NotFound();
            storedProduct.Name = product.Name;
            storedProduct.Price = product.Price;
            storedProduct.Quantity = product.Quantity;
            storedProduct.Description = product.Description;
            return Ok(storedProduct);
        }
        [HttpPut("UpdateProduct")]
        public IActionResult PutBody([FromBody] Product product) => Put(product);

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
