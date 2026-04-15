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
    public class CertificationTypeController : Controller
    {
        private readonly AppDbContext _context;

        public CertificationTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CertificationType
        public async Task<IActionResult> Index()
        {
            return View(await _context.CertificationTypes.ToListAsync());
        }

        // GET: CertificationType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificationType = await _context.CertificationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificationType == null)
            {
                return NotFound();
            }

            return View(certificationType);
        }

        // GET: CertificationType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CertificationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id")] CertificationType certificationType)
        {
            if (ModelState.IsValid)
            {
                certificationType.Id = Guid.NewGuid();
                _context.Add(certificationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(certificationType);
        }

        // GET: CertificationType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificationType = await _context.CertificationTypes.FindAsync(id);
            if (certificationType == null)
            {
                return NotFound();
            }
            return View(certificationType);
        }

        // POST: CertificationType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Id")] CertificationType certificationType)
        {
            if (id != certificationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certificationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificationTypeExists(certificationType.Id))
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
            return View(certificationType);
        }

        // GET: CertificationType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificationType = await _context.CertificationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificationType == null)
            {
                return NotFound();
            }

            return View(certificationType);
        }

        // POST: CertificationType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var certificationType = await _context.CertificationTypes.FindAsync(id);
            if (certificationType != null)
            {
                _context.CertificationTypes.Remove(certificationType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificationTypeExists(Guid id)
        {
            return _context.CertificationTypes.Any(e => e.Id == id);
        }
    }
}
