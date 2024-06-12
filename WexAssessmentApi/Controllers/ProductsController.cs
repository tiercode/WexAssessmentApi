using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Products;
using Domain.TestUsers;
using Persistence;
using Persistence.Repositories;
using WexAssessmentApi.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;

namespace WexAssessmentApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController>? _logger;
        private readonly ITokenServiceAsync _tokenService;
        private int _testProductDataLimit = 50;
        private readonly ProductRepository? _productRepository;
        private static ApplicationContext? _context;
        private List<TestUser> _users;

        public ProductsController(ITokenServiceAsync tokenService, ILogger<ProductsController>? logger)
        {
            _users = new List<TestUser>();
            _logger = logger;
            _tokenService = tokenService;

            try
            {
                GenerateTestProductData();
                _productRepository = new ProductRepository(_context);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register()
        {
            using var client = new HttpClient();

            try
            {
                var token = await _tokenService.GetTokenAsync("read");
                
                client.SetBearerToken(token.AccessToken);
                return Ok(token.AccessToken);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }

        }

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult> Get(int page = 1, int pageSize = 25) 
        {
            return Ok(await _productRepository.GetAllAsync(page, pageSize));
        }

        [HttpGet()]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _productRepository.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("GetProductsByCategoryAsync/{category}")]
        public async Task<ActionResult> GetProductsByCategoryAsync([FromRoute] string category)
        {
            return Ok(await _productRepository.GetProductsByCategoryAsync(category));
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post( Product? product)
        {
            try
            {
                await _productRepository.AddAsync(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Product successfully added.");
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put(Product? newProduct)
        {
            try
            {
                // Get the product entity from db based on the id
                var product = await _productRepository.GetByIdAsync(newProduct.Id);

                // Update entity with modifications
                product.Name = newProduct.Name;
                product.Price = newProduct.Price;
                product.Category = newProduct.Category;
                product.StockQuantity = newProduct.StockQuantity;

                // Save modified entity back to the db
                await _productRepository.UpdateAsync(product);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

            return Ok("Product successfully updated.");
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
            }
            catch (Exception ex) 
            { 
            
            }

            return Ok("Prduct successfully deleted.");
        }

        /// <summary>
        /// Produce test product data
        /// </summary>
        private void GenerateTestProductData()
        {
            Random rnd = new Random();

            if (_context == null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                    .UseInMemoryDatabase(databaseName: "Products");

                _context = new ApplicationContext(optionsBuilder.Options);

                for (int i = 1; i <= _testProductDataLimit; i++)
                {
                    var product = new Product()
                    {
                        Id = i,
                        Name = $"{RandomString(rnd.Next(1, 15))}, {RandomString(rnd.Next(1, 15))}",
                        Price = rnd.Next(-5, 100),
                        Category = RandomString(rnd.Next(1, 25)),
                        StockQuantity = rnd.Next(1, 20)
                    };

                    _context.Products.Add(product);
                }
            }
        }

        /// <summary>
        /// Method to generate a random string of characters
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string RandomString(int length)
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
