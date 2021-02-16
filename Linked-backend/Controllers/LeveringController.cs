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
    [Route("api/leveringen")]
    [ApiController]
    public class LeveringController : Controller
    {
        private readonly ScheduleContext _context;

        public LeveringController(ScheduleContext context)
        {
            _context = context;
        }

        // GET api/leveringen
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Levering>> Get()
        {
            var leveringen = _context.Leveringen.OrderBy(m => m.LaadkadeID);
            return leveringen
                .Include(l => l.Schedule)
                .ThenInclude(l => l.User)
                .ThenInclude(l=> l.Role)
                .Include(l => l.Laadkade)
                .Include(l => l.Leverancier)
                .ThenInclude(l => l.Bedrijf)
                .ToList();
        }
        [Authorize]
        private Levering GetLevering(int id)
        {
            return _context.Leveringen
                .Include(l => l.Schedule)
                .ThenInclude(l => l.User)
                .ThenInclude(l => l.Role)
                .Include(l => l.Laadkade)
                .Include(l => l.Leverancier)
                .ThenInclude(l => l.Bedrijf)
                .FirstOrDefault(a => a.LeveringID == id);
        }

        // GET api/leveringen/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Levering> Get(int id)
        {
            return GetLevering(id);
        }
        // GET api/leveringen/nummerplaat/1abc123
        [HttpGet("nummerplaat/{plaat}")]
        public ActionResult<IEnumerable<Levering>> GetbyPlate(string plaat)
        {
            var leveringen = _context.Leveringen;
            return leveringen
                .Include(l => l.Laadkade)
                .Include(l => l.Leverancier)
                .ThenInclude(l => l.Bedrijf)
                .Include(l => l.Schedule)
                .ThenInclude(l => l.User)
                .ThenInclude(l => l.Role)
                .Where(l => l.Leverancier.Nummerplaat == plaat)
                .ToList();
        }
        // GET api/leveringen/schedule/5
        [HttpGet("schedule/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<Levering>> GetByScheduleID(int id)
        {
            return _context.Leveringen
                .Include(l => l.Schedule)
                .ThenInclude(l => l.User)
                .ThenInclude(l => l.Role)
                .Include(l => l.Laadkade)
                .Include(l => l.Leverancier)
                .ThenInclude(l => l.Bedrijf)
                .Where(l => l.ScheduleID == id)
                .ToList();
        }
        // GET api/leveringen/code/123/nummerplaat/1abc123
        [HttpGet("code/{code}/nummerplaat/{nummerplaat}")]
        public ActionResult<IEnumerable<Levering>> GetbyCode(int code, string nummerplaat)
        {
            var leveringen = _context.Leveringen;
            return leveringen
                .Include(l => l.Laadkade)
                .Include(l => l.Leverancier)
                .ThenInclude(l => l.Bedrijf)
                .Include(l => l.Schedule)
                .ThenInclude(l => l.User)
                .ThenInclude(l => l.Role)
                .Where(l => l.Leverancier.Code == code)
                .Where(l => l.Leverancier.Nummerplaat == nummerplaat)
                .ToList();
        }

        // GET api/leveringen/start/{date}/end/{date}
        [HttpGet("start/{startDatum}/end/{endDatum}")]
        public ActionResult<IEnumerable<Levering>> getBetweenDates(string startDatum, string endDatum)
        {
            var leveringen = _context.Leveringen;
            var start = DateTime.Parse(startDatum);
            var end = DateTime.Parse(endDatum);
            return leveringen
                .Include(l => l.Laadkade)
                .Include(l => l.Leverancier)
                .ThenInclude(l => l.Bedrijf)
                .Include(l => l.Schedule)
                .ThenInclude(l => l.User)
                .ThenInclude(l => l.Role)
                .Where(l => l.Schedule.Datum >= start)
                .Where(l => l.Schedule.Datum <= end)
                .ToList();
        }

        // POST api/leveringen
        [HttpPost]
        [Authorize]
        public ActionResult<Levering> Post([FromBody] Levering value)
        {
            _context.Leveringen.Add(value);
            _context.SaveChanges();
            return GetLevering(value.LeveringID);
        }

        // PUT api/leveringen/5
        [HttpPut("{id}")]
        public ActionResult<Levering> Put(int id, [FromBody] Levering value)
        {
            var levering = _context.Leveringen.SingleOrDefault(a => a.LeveringID == id);
            levering.Omschrijving = value.Omschrijving;
            levering.Laadkade = value.Laadkade;
            levering.LaadkadeID = value.LaadkadeID;
            levering.Leverancier = value.Leverancier;
            levering.LeverancierID = value.LeverancierID;
            levering.IsCompleet = value.IsCompleet;
            levering.Schedule = value.Schedule;
            levering.ScheduleID = value.ScheduleID;
            _context.SaveChanges();
            return GetLevering(levering.LeveringID);
        }

        // DELETE api/leveringen/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Levering> Delete(int id)
        {
            var levering = _context.Leveringen.SingleOrDefault(a => a.LeveringID == id);
            _context.Leveringen.Remove(levering);
            _context.SaveChanges();
            return levering;
        }
    }
}
