using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using event_booking.Data;
using event_booking.Models;

public class JunctionTicketVipsController : Controller
{
    private readonly ApplicationDbContext _applicationDbContext;

    public JunctionTicketVipsController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IActionResult> Index()
    {
        var junctionTicketVips = await _applicationDbContext.Junction_Ticket_VIP.ToListAsync();
        return View(junctionTicketVips);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(JunctionTicketVip junctionTicketVip)
    {
        if (ModelState.IsValid)
        {
            await _applicationDbContext.JunctionTicketVips.AddAsync(junctionTicketVip);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(junctionTicketVip);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var junctionTicketVip = await _applicationDbContext.JunctionTicketVips.FindAsync(id);
        if (junctionTicketVip == null)
        {
            return NotFound();
        }

        return View(junctionTicketVip);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(JunctionTicketVip junctionTicketVip)
    {
        if (ModelState.IsValid)
        {
            _applicationDbContext.Update(junctionTicketVip);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(junctionTicketVip);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var junctionTicketVip = await _applicationDbContext.JunctionTicketVips.FindAsync(id);
        if (junctionTicketVip == null)
        {
            return NotFound();
        }

        _applicationDbContext.JunctionTicketVips.Remove(junctionTicketVip);
        await _applicationDbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}