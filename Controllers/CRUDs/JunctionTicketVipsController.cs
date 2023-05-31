using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using event_booking.Data;

public class JunctionTicketVipsController : Controller
{
    private readonly ApplicationDbContext _applicationDbContext;

    public JunctionTicketVipsController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IActionResult> Index()
    {
        var junctionTicketVips = await _applicationDbContext.Set<Dictionary<string, object>>().FromSqlRaw("SELECT * FROM Junction_Ticket_VIP").ToListAsync();
        return View(junctionTicketVips);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Dictionary<string, object> junctionTicketVip)
    {
        // Perform necessary validations and populate the dictionary object with the required values

        await _applicationDbContext.Database.ExecuteSqlRawAsync("INSERT INTO Junction_Ticket_VIP (TicketId, VipId) VALUES ({ticketId}, {vipId})", junctionTicketVip);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int ticketId, int vipId)
    {
        var junctionTicketVip = await _applicationDbContext.Set<Dictionary<string, object>>().FromSqlRaw("SELECT * FROM Junction_Ticket_VIP WHERE TicketId = {ticketId} AND VipId = {vipId}", ticketId, vipId).FirstOrDefaultAsync();

        if (junctionTicketVip == null)
        {
            return NotFound();
        }

        return View(junctionTicketVip);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Dictionary<string, object> junctionTicketVip)
    {
        // Perform necessary validations and update the dictionary object with the modified values

        await _applicationDbContext.Database.ExecuteSqlRawAsync("UPDATE Junction_Ticket_VIP SET ... WHERE TicketId = {ticketId} AND VipId = {vipId}", junctionTicketVip);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int ticketId, int vipId)
    {
        await _applicationDbContext.Database.ExecuteSqlRawAsync("DELETE FROM Junction_Ticket_VIP WHERE TicketId = {ticketId} AND VipId = {vipId}", ticketId, vipId);
        return RedirectToAction("Index");
    }
}
