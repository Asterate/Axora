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
    public class MaterialDistributionController : Controller
    {
        private readonly AppDbContext _context;

        public MaterialDistributionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MaterialDistribution
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MaterialDistributions.Include(m => m.Enrollment).Include(m => m.Material);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MaterialDistribution/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialDistribution = await _context.MaterialDistributions
                .Include(m => m.Enrollment)
                .Include(m => m.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materialDistribution == null)
            {
                return NotFound();
            }

            return View(materialDistribution);
        }

        // GET: MaterialDistribution/Create
        public IActionResult Create()
        {
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason");
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "MaterialDescription");
            return View();
        }

        // POST: MaterialDistribution/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId,MaterialId,MaterialDistributionCreatedAt,MaterialDistributionModifiedAt,MaterialDistributionDeletedAt,Id")] MaterialDistribution materialDistribution)
        {
            if (ModelState.IsValid)
            {
                materialDistribution.Id = Guid.NewGuid();
                _context.Add(materialDistribution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason", materialDistribution.EnrollmentId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "MaterialDescription", materialDistribution.MaterialId);
            return View(materialDistribution);
        }

        // GET: MaterialDistribution/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialDistribution = await _context.MaterialDistributions.FindAsync(id);
            if (materialDistribution == null)
            {
                return NotFound();
            }
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason", materialDistribution.EnrollmentId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "MaterialDescription", materialDistribution.MaterialId);
            return View(materialDistribution);
        }

        // POST: MaterialDistribution/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EnrollmentId,MaterialId,MaterialDistributionCreatedAt,MaterialDistributionModifiedAt,MaterialDistributionDeletedAt,Id")] MaterialDistribution materialDistribution)
        {
            if (id != materialDistribution.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialDistribution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialDistributionExists(materialDistribution.Id))
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
            ViewData["EnrollmentId"] = new SelectList(_context.Enrollments, "Id", "EnrollmentSeason", materialDistribution.EnrollmentId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "MaterialDescription", materialDistribution.MaterialId);
            return View(materialDistribution);
        }

        // GET: MaterialDistribution/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialDistribution = await _context.MaterialDistributions
                .Include(m => m.Enrollment)
                .Include(m => m.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materialDistribution == null)
            {
                return NotFound();
            }

            return View(materialDistribution);
        }

        // POST: MaterialDistribution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var materialDistribution = await _context.MaterialDistributions.FindAsync(id);
            if (materialDistribution != null)
            {
                _context.MaterialDistributions.Remove(materialDistribution);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialDistributionExists(Guid id)
        {
            return _context.MaterialDistributions.Any(e => e.Id == id);
        }
    }
}
