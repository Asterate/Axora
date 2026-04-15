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
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CertificationController : Controller
    {
        private readonly AppDbContext _context;

        public CertificationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Certification
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Certifications.Include(c => c.CertificationType).Include(c => c.InstituteUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Certification/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certification = await _context.Certifications
                .Include(c => c.CertificationType)
                .Include(c => c.InstituteUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certification == null)
            {
                return NotFound();
            }

            return View(certification);
        }

        // GET: Certification/Create
        public IActionResult Create()
        {
            ViewData["CertificationTypeId"] = new SelectList(_context.CertificationTypes, "Id", "Name");
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id");
            return View();
        }

        // POST: Certification/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CertificationName,HandedOver,Expired,InstituteUserId,CertificationTypeId,Id")] Certification certification)
        {
            if (ModelState.IsValid)
            {
                certification.Id = Guid.NewGuid();
                _context.Add(certification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CertificationTypeId"] = new SelectList(_context.CertificationTypes, "Id", "Name", certification.CertificationTypeId);
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", certification.InstituteUserId);
            return View(certification);
        }

        // GET: Certification/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certification = await _context.Certifications.FindAsync(id);
            if (certification == null)
            {
                return NotFound();
            }
            ViewData["CertificationTypeId"] = new SelectList(_context.CertificationTypes, "Id", "Name", certification.CertificationTypeId);
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", certification.InstituteUserId);
            return View(certification);
        }

        // POST: Certification/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CertificationName,HandedOver,Expired,InstituteUserId,CertificationTypeId,Id")] Certification certification)
        {
            if (id != certification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificationExists(certification.Id))
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
            ViewData["CertificationTypeId"] = new SelectList(_context.CertificationTypes, "Id", "Name", certification.CertificationTypeId);
            ViewData["InstituteUserId"] = new SelectList(_context.InstituteUsers, "Id", "Id", certification.InstituteUserId);
            return View(certification);
        }

        // GET: Certification/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certification = await _context.Certifications
                .Include(c => c.CertificationType)
                .Include(c => c.InstituteUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certification == null)
            {
                return NotFound();
            }

            return View(certification);
        }

        // POST: Certification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var certification = await _context.Certifications.FindAsync(id);
            if (certification != null)
            {
                _context.Certifications.Remove(certification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificationExists(Guid id)
        {
            return _context.Certifications.Any(e => e.Id == id);
        }
    }
}
