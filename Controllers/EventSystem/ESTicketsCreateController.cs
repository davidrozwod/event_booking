using event_booking.Data;
using event_booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace event_booking.Controllers.EventSystem
{
    public class ESTicketsCreateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ESTicketsCreateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events.OrderBy(e => e.Name), "EventId", "Name");
            //ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");
            ViewData["SeatId"] = new SelectList(_context.Seats.OrderBy(s => s.SeatNumber), "SeatId", "SeatNumber");

            return View("~/Views/EventSystem/Tickets/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public IActionResult CreateWithAutomation(Ticket ticket)
        {
            //Variables to track the number of seats and tickets created. Also sets false for ticketsExist.
            int seatsCreated = 0;
            int ticketsCreated = 0;
            bool ticketsExist = false;

            ModelState.Remove("Seat");
            ModelState.Remove("Event");
            ModelState.Remove("Venue");            
            if (ModelState.IsValid)
            {
                // Fetch the event
                Event @event = _context.Events.Find(ticket.EventId);

                if (@event != null)
                {
                    // Assign the venue ID from the event
                    ticket.VenueId = @event.VenueId;

                    Venue venue = _context.Venues.Find(ticket.VenueId);

                    if (venue != null)
                    {
                        //Sets the limit of seats creation to 10, to prevent overloading the database.
                        //This will also limit the number of tickets that can be created.

                        //int seatCapacity = venue.SeatCapacity;
                        int seatCapacity = 10;

                        // Check the current number of seats for the venue
                        int currentSeatCount = _context.Seats.Count(s => s.VenueId == ticket.VenueId);

                        // Fetch all the sections
                        List<Section> sections = _context.Sections.ToList();

                        // Fetch all multipliers
                        List<decimal> multipliers = sections.Select(s => s.PriceMultiplier).ToList();

                        //Create seats, if the current seat count is less than the seat capacity
                        //The SectionId is assigned to the seat based on the number of seats per section
                        if (currentSeatCount < seatCapacity)
                        {
                            int remainingSeatCount = seatCapacity - currentSeatCount;

                            // Get the highest seat number for the venue
                            int maxSeatNumber = _context.Seats
                                .Where(s => s.VenueId == ticket.VenueId)
                                .Max(s => (int?)s.SeatNumber) ?? 0;

                            // Calculate the number of seats per section
                            int seatsPerSection = remainingSeatCount / sections.Count;

                            // Calculate the remaining seats after even distribution
                            int remainingSeats = remainingSeatCount % sections.Count;

                            // Create new seats for the remaining count starting from the next seat number
                            for (int i = 0; i < sections.Count; i++)
                            {
                                for (int j = 0; j < seatsPerSection; j++)
                                {
                                    int seatNumber = maxSeatNumber + i * seatsPerSection + j + 1;

                                    Seat newSeat = new Seat
                                    {
                                        VenueId = ticket.VenueId,
                                        SeatNumber = seatNumber,
                                        SectionId = sections[i].SectionId  // Assign the seat to the current section
                                    };

                                    _context.Seats.Add(newSeat);
                                    seatsCreated++; // increment seatsCreated
                                }
                            }

                            // Distribute remaining seats
                            for (int i = 0; i < remainingSeats; i++)
                            {
                                int seatNumber = maxSeatNumber + seatsPerSection * sections.Count + i + 1;

                                Seat newSeat = new Seat
                                {
                                    VenueId = ticket.VenueId,
                                    SeatNumber = seatNumber,
                                    SectionId = sections[i].SectionId  // Assign the seat to the current section
                                };

                                _context.Seats.Add(newSeat);
                                seatsCreated++; // increment seatsCreated
                            }

                            _context.SaveChanges();
                        }

                        // Check if tickets already exist for the event
                        ticketsExist = _context.Tickets.Any(t => t.EventId == ticket.EventId);

                        //If Tickets already exist display to user
                        ViewBag.TicketsExist = ticketsExist;

                        if (!ticketsExist)
                        {
                            // Create a ticket for each seat
                            for (int i = 1; i <= seatCapacity; i++)
                            {
                                Seat seat = _context.Seats.FirstOrDefault(s => s.VenueId == ticket.VenueId && s.SeatNumber == i);

                                if (seat != null)
                                {
                                    Section seatSection = _context.Sections.FirstOrDefault(s => s.SectionId == seat.SectionId);

                                    if (seatSection != null)
                                    {
                                        Ticket newTicket = new Ticket
                                        {
                                            EventId = ticket.EventId,
                                            VenueId = ticket.VenueId,
                                            SeatId = seat.SeatId,
                                            BasePrice = ticket.BasePrice,
                                            TicketPrice = ticket.BasePrice * seatSection.PriceMultiplier
                                        };

                                        _context.Tickets.Add(newTicket);
                                        ticketsCreated++; // increment ticketsCreated
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Display a message indicating that tickets already exist for the event
                            ModelState.AddModelError(string.Empty, "Tickets already exist for this event.");

                            ViewData["EventId"] = new SelectList(_context.Events.OrderBy(e => e.Name), "EventId", "Name");
                            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");

                            return View("~/Views/EventSystem/Tickets/Create.cshtml", ticket);
                        }

                        _context.SaveChanges();


                        ViewBag.SeatsCreated = seatsCreated;
                        ViewBag.TicketsCreated = ticketsCreated;

                        ViewData["EventId"] = new SelectList(_context.Events.OrderBy(e => e.Name), "EventId", "Name");
                        ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");
                    }
                }


                return View("~/Views/EventSystem/Tickets/Create.cshtml", ticket);
            }

            ViewData["EventId"] = new SelectList(_context.Events.OrderBy(e => e.Name), "EventId", "Name");
            //ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");

            return View("~/Views/EventSystem/Tickets/Create.cshtml", ticket);
        }
    }
}
