using event_booking.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_booking.Models;

namespace event_booking.Controllers
{
    public class ProfessionalAndGovernmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessionalAndGovernmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var subCategoryIds = new List<int> { 51, 52, 53, 54, 55, 56, 57, 58, 59, 92 }; // IDs of your 10 subcategories

            var events = _context.Events
                .Where(e => subCategoryIds.Contains(e.EventCategory.EventCategoryId)) // Filter by subcategory ID
                .Include(e => e.EventCategory)
                 .Include(e => e.Organizer)
                .ToList();

            return View("~/Views/EventCategories/ProfessionalAndGovernment.cshtml", events);
        }
        public async Task<IActionResult> IndustryConferencesAndSummits()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 51)
                .ToListAsync();

            return View("~/Views/EventCategories/ProfessionalAndGovernment/IndustryConferencesAndSummits.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> WorkshopsAndTrainingSessions()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 52)
                .ToListAsync();

            return View("~/Views/EventCategories/ProfessionalAndGovernment/WorkshopsAndTrainingSessions.cshtml", concertsAndGigs);
        }

        public async Task<IActionResult> ProductLaunchesAndDemonstrations()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 53)
                .ToListAsync();

            return View("~/Views/EventCategories/ProfessionalAndGovernment/ProductLaunchesAndDemonstrations.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> BusinessNetworkingEvents()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 54)
                .ToListAsync();

            return View("~/Views/EventCategories/ProfessionalAndGovernment/BusinessNetworkingEvents.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> AcademicResearchPresentation()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 55)
                .ToListAsync();

            return View("~/Views/EventCategories/ProfessionalAndGovernment/AcademicResearchPresentation.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> PanelDiscussionsAndTalks()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 56)
                .ToListAsync();

            return View("~/Views/EventCategories/ProfessionalAndGovernment/PanelDiscussionsAndTalks.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> TradeShowsAndExhibition()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 57)
                .ToListAsync();

            return View("~/Views/EventCategories/ProfessionalAndGovernment/TradeShowsAndExhibition.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> GovernmentConferencesAndForums()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 58)
                .ToListAsync();

            return View("~/Views/EventCategories/ProfessionalAndGovernment/GovernmentConferencesAndForums.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> PoliticalRalliesAndCampaignEvents()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 59)
                .ToListAsync();

            return View("~/Views/EventCategories/ProfessionalAndGovernment/PoliticalRalliesAndCampaignEvents.cshtml", concertsAndGigs);
        }
        public async Task<IActionResult> ProfessionalAssociationMeetings()
        {
            var concertsAndGigs = await _context.Events
                .Include(e => e.Organizer)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == 92)
                .ToListAsync();

            return View("~/Views/EventCategories/ProfessionalAndGovernment/ProfessionalAssociationMeetings.cshtml", concertsAndGigs);
        }
    }
}