using event_booking.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_booking.Models;

namespace event_booking.Controllers
{
    public class FamilyAndEducationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FamilyAndEducationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var subCategoryIds = new List<int> { 82, 83, 84, 85, 86, 87, 88, 89, 90, 91}; // IDs of your 10 subcategories

            var events = _context.Events
                .Where(e => subCategoryIds.Contains(e.EventCategory.EventCategoryId)) // Filter by subcategory ID
                .Include(e => e.EventCategory)
                 .Include(e => e.Organizer)
                .ToList();

            return View("~/Views/EventCategories/FamilyAndEducation.cshtml", events);
        }
        public async Task<IActionResult> ChildrensStorytellingAndPuppetShows()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 82)
                .ToListAsync();

            return View("~/Views/EventCategories/FamilyAndEducation/ChildrensStorytellingAndPuppetShows.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ScienceExperimentsAndExhibitions()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 83)
                .ToListAsync();

            return View("~/Views/EventCategories/FamilyAndEducation/ScienceExperimentsAndExhibitions.cshtml", concertsAndGigs);
        }

        public async Task<IActionResult> ParentingWorkshopsAndSeminars()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 84)
                .ToListAsync();

            return View("~/Views/EventCategories/FamilyAndEducation/ParentingWorkshopsAndSeminars.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> EducationalWorkshopsAndClasses()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 85)
                .ToListAsync();

            return View("~/Views/EventCategories/FamilyAndEducation/EducationalWorkshopsAndClasses.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> SchoolPlaysAndPerformances()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 86)
                .ToListAsync();

            return View("~/Views/EventCategories/FamilyAndEducation/SchoolPlaysAndPerformances.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> FamilyFunDaysAndPicnics()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 87)
                .ToListAsync();

            return View("~/Views/EventCategories/FamilyAndEducation/FamilyFunDaysAndPicnics.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> CraftAndArtWorkshopsforKids()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 88)
                .ToListAsync();

            return View("~/Views/EventCategories/FamilyAndEducation/CraftAndArtWorkshopsforKids.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ParentTeacherConferences()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 89)
                .ToListAsync();

            return View("~/Views/EventCategories/FamilyAndEducation/ParentTeacherConferences.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> CollegeFairsAndEducationalExpos()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 90)
                .ToListAsync();

            return View("~/Views/EventCategories/FamilyAndEducation/CollegeFairsAndEducationalExpos.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> EducationalFieldTrips()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 91)
                .ToListAsync();

            return View("~/Views/EventCategories/FamilyAndEducation/EducationalFieldTrips.cshtml", concertsAndGigs);
        }
    }
}