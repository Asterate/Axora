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
    public class CompanyConfigController : Controller
    {
        private readonly AppDbContext _context;

        public CompanyConfigController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CompanyConfig
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CompanyConfigs.Include(c => c.Company);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CompanyConfig/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyConfig = await _context.CompanyConfigs
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyConfig == null)
            {
                return NotFound();
            }

            return View(companyConfig);
        }

        // GET: CompanyConfig/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress");
            return View();
        }

        // POST: CompanyConfig/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,CompanyConfigEnabled,CompanyConfigTimeZone,CompanyConfigAllowOneOnOneSessions,CompanyConfigEnableMaterialTracking,CompanyConfigThemeColour,CompanyConfigCompanyLogoPath,CompanyConfigCreatedAt,CompanyConfigUpdatedAt,CompanyConfigMaxStudentsPerCourse,CompanyConfigEnableCertificates,Id")] CompanyConfig companyConfig)
        {
            if (ModelState.IsValid)
            {
                companyConfig.Id = Guid.NewGuid();
                _context.Add(companyConfig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", companyConfig.CompanyId);
            return View(companyConfig);
        }

        // GET: CompanyConfig/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyConfig = await _context.CompanyConfigs.FindAsync(id);
            if (companyConfig == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", companyConfig.CompanyId);
            return View(companyConfig);
        }

        // POST: CompanyConfig/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CompanyId,CompanyConfigEnabled,CompanyConfigTimeZone,CompanyConfigAllowOneOnOneSessions,CompanyConfigEnableMaterialTracking,CompanyConfigThemeColour,CompanyConfigCompanyLogoPath,CompanyConfigCreatedAt,CompanyConfigUpdatedAt,CompanyConfigMaxStudentsPerCourse,CompanyConfigEnableCertificates,Id")] CompanyConfig companyConfig)
        {
            if (id != companyConfig.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyConfig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyConfigExists(companyConfig.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyAddress", companyConfig.CompanyId);
            return View(companyConfig);
        }

        // GET: CompanyConfig/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyConfig = await _context.CompanyConfigs
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyConfig == null)
            {
                return NotFound();
            }

            return View(companyConfig);
        }

        // POST: CompanyConfig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyConfig = await _context.CompanyConfigs.FindAsync(id);
            if (companyConfig != null)
            {
                _context.CompanyConfigs.Remove(companyConfig);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyConfigExists(Guid id)
        {
            return _context.CompanyConfigs.Any(e => e.Id == id);
        }
    }
}
