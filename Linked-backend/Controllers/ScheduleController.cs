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
    [Route("api/schedules")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly ScheduleContext _context;

        public ScheduleController(ScheduleContext context)
        {
            _context = context;
        }

        // GET api/schedules
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Schedule>> Get()
        {
            var schedules = _context.Schedules.OrderBy(m => m.Datum);
            return schedules
                .Include(l => l.User)
                .ThenInclude(l => l.Role)
                .ToList();
        }
        [Authorize]
        private Schedule GetSchedule(int id)
        {
            return _context.Schedules
                .Include(l => l.User)
                .ThenInclude(l => l.Role)
                .FirstOrDefault(a => a.ScheduleID == id);
        }

        // GET api/schedules/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Schedule> Get(int id)
        {
            return GetSchedule(id);
        }

        // GET api/schedule/start/{date}/end/{date}
        [HttpGet("start/{startDatum}/end/{endDatum}")]
        [Authorize]
        public ActionResult<IEnumerable<Schedule>> getBetweenDates(string startDatum, string endDatum)
        {
            var schedules = _context.Schedules;
            var start = DateTime.Parse(startDatum);
            var end = DateTime.Parse(endDatum);
            return schedules
                .Include(l => l.User)
                .ThenInclude(l => l.Role)
                .Where(l => l.Datum >= start)
                .Where(l => l.Datum <= end)
                .ToList();
        }

        // POST api/schedules
        [HttpPost]
        [Authorize]
        public ActionResult<Schedule> Post([FromBody] Schedule value)
        {
            _context.Schedules.Add(value);
            _context.SaveChanges();
            return GetSchedule(value.ScheduleID);
        }

        // PUT api/schedules/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Schedule> Put(int id, [FromBody] Schedule value)
        {
            var schedule = _context.Schedules.SingleOrDefault(a => a.ScheduleID == id);
            schedule.Code = value.Code;
            schedule.Datum = value.Datum;
            schedule.Opmerking = value.Opmerking;
            schedule.User = value.User;
            schedule.UserID = value.UserID;
            _context.SaveChanges();
            return GetSchedule(schedule.ScheduleID);
        }

        // DELETE api/schedules/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Schedule> Delete(int id)
        {
            var schedule = _context.Schedules.SingleOrDefault(a => a.ScheduleID == id);
            _context.Schedules.Remove(schedule);
            _context.SaveChanges();
            return schedule;
        }
    }
}
