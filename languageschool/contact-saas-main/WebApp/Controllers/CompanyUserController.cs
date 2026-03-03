using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Entities;

namespace WebApp.Controllers
{
    public class CompanyUserController : Controller
    {
        private readonly AppDbContext _context;

        public CompanyUserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CompanyUser
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CompanyUsers.Include(c => c.AppUser).Include(c => c.Company);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CompanyUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyUser = await _context.CompanyUsers
                .Include(c => c.AppUser)
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyUser == null)
            {
                return NotFound();
            }

            return View(companyUser);
        }

        // GET: CompanyUser/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress");
            return View();
        }

        // POST: CompanyUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,AppUserId,Id")] CompanyUser companyUser)
        {
            if (ModelState.IsValid)
            {
                companyUser.Id = Guid.NewGuid();
                _context.Add(companyUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", companyUser.AppUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", companyUser.CompanyId);
            return View(companyUser);
        }

        // GET: CompanyUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyUser = await _context.CompanyUsers.FindAsync(id);
            if (companyUser == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", companyUser.AppUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", companyUser.CompanyId);
            return View(companyUser);
        }

        // POST: CompanyUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CompanyId,AppUserId,Id")] CompanyUser companyUser)
        {
            if (id != companyUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyUserExists(companyUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", companyUser.AppUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", companyUser.CompanyId);
            return View(companyUser);
        }

        // GET: CompanyUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyUser = await _context.CompanyUsers
                .Include(c => c.AppUser)
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyUser == null)
            {
                return NotFound();
            }

            return View(companyUser);
        }

        // POST: CompanyUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyUser = await _context.CompanyUsers.FindAsync(id);
            if (companyUser != null)
            {
                _context.CompanyUsers.Remove(companyUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyUserExists(Guid id)
        {
            return _context.CompanyUsers.Any(e => e.Id == id);
        }
    }
}
