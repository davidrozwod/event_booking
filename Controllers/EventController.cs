using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_booking.Models;
using event_booking.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace event_booking.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Event
        public IActionResult Index()
        {
            var events = _context.Events.Include(e => e.EventCategory).Include(e => e.Organizer);
            return View(events.ToList());
        }

        // GET: Event/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _context.Events
                .Include(e => e.EventCategory)
                .Include(e => e.Organizer)
                .FirstOrDefault(e => e.EventId == id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            // Populate any dropdowns or necessary data for the create view
            // For example, you can fetch EventCategory and Organizer data from the database and pass it to the view
            // If the model is not valid, return the create view with validation errors
            ViewBag.Organizers = _context.Organizers.ToList();
            ViewBag.EventCategories = _context.EventCategories.ToList();

            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Add(@event);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

                // Set up necessary data for the create view
                ViewBag.Organizers = _context.Organizers.ToList();
                ViewBag.EventCategories = _context.EventCategories.ToList();

                // Return the create view with validation errors
                return View(@event);
        }


        // GET: Event/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _context.Events.Find(id);

            if (@event == null)
            {
                return NotFound();
            }
            ViewBag.Organizers = _context.Organizers.ToList(); // Populate Organizers
            ViewBag.EventCategories = _context.EventCategories.ToList();
            return View(@event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Events.Update(@event);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(@event);
        }

        // GET: Event/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _context.Events
                .FirstOrDefault(e => e.EventId == id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var @event = _context.Events.Find(id);
            _context.Events.Remove(@event);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
