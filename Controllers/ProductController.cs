using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPlusSport.API.Models;
using HPlusSportAPI.Classes;
using HPlusSportAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSportAPI.Controllers
{
    [Route("Controller")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ShopContext _Context;
        public ProductController(ShopContext context)
        {
            _Context = context;

            _Context.Database.EnsureCreated();

        }
        [HttpGet]
        /*    public IEnumerable<Product> GetAllProducts()
            {
                return _Context.Products.ToArray();
            }*/
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
        {
            IQueryable<Product> products = _Context.Products;

            if (queryParameters.MinPrice != null && queryParameters.MaxPrice != null)
            {
                products = products.Where(
                    p => p.Price >= queryParameters.MinPrice.Value &&
                         p.Price <= queryParameters.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(queryParameters.Sku))
            {
                products = products.Where(p => p.Sku == queryParameters.Sku);

            }
            //for serach items 
            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(
                          p => p.Name.ToLower().Contains(queryParameters.Name.ToLower()));
            }
            //sort values checked 
            if (!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                if (typeof(Product).GetProperty(queryParameters.SortBy) != null)
                {
                    products = products.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
                }

            }

            products = products
                .Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);


            return Ok(await products.ToArrayAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _Context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
            _Context.Products.Add(product);
            await _Context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id },
                product);
        }

        [HttpPut("{id})")]
        public async Task <IActionResult> PutProduct ([FromRoute]int id,[FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _Context.Entry(product).State = EntityState.Modified;
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_Context.Products.Find(id) == null)
                {
                    return NotFound();

                }
                throw;
            }
            return NoContent();
         
        }

        [HttpDelete("{id})")]
        public async Task <ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _Context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _Context.Products.Remove(product);
            await _Context.SaveChangesAsync();

            return product;
        }


       /*public IActionResult Index()
        {
         //   return View(Product);
        }*/
    }
}
