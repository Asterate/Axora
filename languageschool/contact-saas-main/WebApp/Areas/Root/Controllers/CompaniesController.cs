using App.BLL;
using App.DAL.EF;
using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Root.Controllers;

[Area("Root")]
[Authorize(Roles = "root")]
public class CompaniesController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public CompaniesController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Root/Companies/Pending
    public async Task<IActionResult> Pending()
    {
        var pendingCompanies = await CompanyAdmin.GetPendingCompanies(_context);
        return View(pendingCompanies);
    }

    // GET: Root/Companies/Log
    public async Task<IActionResult> Log()
    {
        var processedCompanies = await CompanyAdmin.GetAllProcessedCompanies(_context);
        return View(processedCompanies);
    }

    // POST: Root/Companies/Approve/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(Guid id)
    {
        await CompanyAdmin.ApproveCompany(_context, _userManager, id);
        
        return RedirectToAction(nameof(Pending));
    }

    // POST: Root/Companies/Reject/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(Guid id)
    {
        try
        {
            await CompanyAdmin.RejectCompany(_context, id);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error rejecting company: {ex.Message}");
        }
        
        return RedirectToAction(nameof(Pending));
    }

    // GET: Root/Companies/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            return NotFound();
        }
        return View(company);
    }

    // POST: Root/Companies/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("CompanyName,CompanyAddress,CompanyPhoneNumber,CompanyEmail,Id")] Company company)
    {
        if (id != company.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(company);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(company.Id))
                {
                    return NotFound();
                }
            }
            return RedirectToAction(nameof(Log));
        }
        return View(company);
    }

    // GET: Root/Companies/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await _context.Companies
            .FirstOrDefaultAsync(m => m.Id == id);
        if (company == null)
        {
            return NotFound();
        }

        return View(company);
    }

    // POST: Root/Companies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            await CompanyAdmin.DeleteCompany(_context, id);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error deleting company: {ex.Message}");
            return View(await _context.Companies.FindAsync(id));
        }
        
        return RedirectToAction(nameof(Log));
    }

    private bool CompanyExists(Guid id)
    {
        return _context.Companies.Any(e => e.Id == id);
    }
}
