using event_booking.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_booking.Models;

namespace event_booking.Controllers
{
    public class CharityAndCausesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharityAndCausesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var subCategoryIds = new List<int> { 62, 63, 64, 65, 66, 67, 68, 69, 70, 71 }; // IDs of your 10 subcategories

            var events = _context.Events
                .Where(e => subCategoryIds.Contains(e.EventCategory.EventCategoryId)) // Filter by subcategory ID
                .Include(e => e.EventCategory)
                 .Include(e => e.Organizer)
                .ToList();

            return View("~/Views/EventCategories/CharityAndCauses.cshtml", events);
        }
        public async Task<IActionResult> FundraisingGalasAndDinners()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 62)
                .ToListAsync();

            return View("~/Views/EventCategories/CharityAndCauses/FundraisingGalasAndDinners.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> CharityRunsAndWalks()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 63)
                .ToListAsync();

            return View("~/Views/EventCategories/CharityAndCauses/CharityRunsAndWalks.cshtml", concertsAndGigs);
        }

        public async Task<IActionResult> VolunteerServiceEvents()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 64)
                .ToListAsync();

            return View("~/Views/EventCategories/CharityAndCauses/VolunteerServiceEvents.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> AwarenessCampaignsAndWalks()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 65)
                .ToListAsync();

            return View("~/Views/EventCategories/CharityAndCauses/AwarenessCampaignsAndWalks.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> BenefitConcertsAndPerformances()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 66)
                .ToListAsync();

            return View("~/Views/EventCategories/CharityAndCauses/BenefitConcertsAndPerformances.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> CommunityOutreachPrograms()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 67)
                .ToListAsync();

            return View("~/Views/EventCategories/CharityAndCauses/CommunityOutreachPrograms.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> EnvironmentalCleanupEvents()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 68)
                .ToListAsync();

            return View("~/Views/EventCategories/CharityAndCauses/EnvironmentalCleanupEvents.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> HumanitaritanReliefEvents()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 69)
                .ToListAsync();

            return View("~/Views/EventCategories/CharityAndCauses/HumanitaritanReliefEvents.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> AnimalWelfareFundraisers()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 70)
                .ToListAsync();

            return View("~/Views/EventCategories/CharityAndCauses/AnimalWelfareFundraisers.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> SocialJusticeActivsmEvents()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 71)
                .ToListAsync();

            return View("~/Views/EventCategories/CharityAndCauses/SocialJusticeActivsmEvents.cshtml", concertsAndGigs);
        }
    }
}