using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Linked_backend.Data;
using Linked_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Linked_backend.Controllers
{
    [Route("api/producten")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ScheduleContext _context;

        public ProductController(ScheduleContext context)
        {
            _context = context;
        }

        // GET api/producten
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var producten = _context.Producten.OrderBy(m => m.Naam);
            return producten
                .Include(l => l.Levering)
                .ThenInclude(l => l.Laadkade)
                .Include(l => l.Levering)
                .ThenInclude(l => l.Schedule)
                .ThenInclude(l => l.User)
                .ThenInclude(l => l.Role)
                .Include(l => l.Levering)
                .ThenInclude(l => l.Leverancier)
                .ThenInclude(l => l.Bedrijf)
                .ToList();
        }
        [Authorize]
        private Product GetProduct(int id)
        {
            return _context.Producten
                .Include(l => l.Levering)
                .ThenInclude(l => l.Laadkade)
                .Include(l => l.Levering)
                .ThenInclude(l => l.Schedule)
                .ThenInclude(l => l.User)
                .ThenInclude(l => l.Role)
                .Include(l => l.Levering)
                .ThenInclude(l => l.Leverancier)
                .ThenInclude(l => l.Bedrijf)
                .FirstOrDefault(a => a.ProductID == id);
        }

        // GET api/producten/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Product> Get(int id)
        {
            return GetProduct(id);
        }

        // GET api/producten/levering/5
        [HttpGet("levering/{id}")]
        public ActionResult<IEnumerable<Product>> GetByLeveringID(int id)
        {
            var producten = _context.Producten.OrderBy(m => m.Naam);
            return producten
                .Include(l => l.Levering)
                .ThenInclude(l => l.Laadkade)
                .Include(l => l.Levering)
                .ThenInclude(l => l.Schedule)
                .ThenInclude(l => l.User)
                .ThenInclude(l => l.Role)
                .Include(l => l.Levering)
                .ThenInclude(l => l.Leverancier)
                .ThenInclude(l => l.Bedrijf)
                .Where(m => m.LeveringID == id)
                .ToList();
        }

        // POST api/producten
        [HttpPost]
        [Authorize]
        public ActionResult<Product> Post([FromBody] Product value)
        {
            _context.Producten.Add(value);
            _context.SaveChanges();
            return GetProduct(value.ProductID);
        }

        // PUT api/producten/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Product> Put(int id, [FromBody] Product value)
        {
            var product = _context.Producten.SingleOrDefault(a => a.ProductID == id);
            product.Levering = value.Levering;
            product.LeveringID = value.LeveringID;
            product.Naam = value.Naam;
            _context.SaveChanges();
            return GetProduct(product.ProductID);
        }

        // DELETE api/producten/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Product> Delete(int id)
        {
            var product = _context.Producten.SingleOrDefault(a => a.ProductID == id);
            _context.Producten.Remove(product);
            _context.SaveChanges();
            return product;
        }
    }
}
