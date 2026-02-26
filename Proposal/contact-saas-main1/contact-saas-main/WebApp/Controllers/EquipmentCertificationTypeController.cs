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
    public class EquipmentCertificationTypeController : Controller
    {
        private readonly AppDbContext _context;

        public EquipmentCertificationTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EquipmentCertificationType
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EquipmentCertificationTypes.Include(e => e.CertificationType).Include(e => e.Equipment);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EquipmentCertificationType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentCertificationType = await _context.EquipmentCertificationTypes
                .Include(e => e.CertificationType)
                .Include(e => e.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentCertificationType == null)
            {
                return NotFound();
            }

            return View(equipmentCertificationType);
        }

        // GET: EquipmentCertificationType/Create
        public IActionResult Create()
        {
            ViewData["CertificationTypeId"] = new SelectList(_context.CertificationTypes, "Id", "Name");
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName");
            return View();
        }

        // POST: EquipmentCertificationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentId,CertificationTypeId,Id")] EquipmentCertificationType equipmentCertificationType)
        {
            if (ModelState.IsValid)
            {
                equipmentCertificationType.Id = Guid.NewGuid();
                _context.Add(equipmentCertificationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CertificationTypeId"] = new SelectList(_context.CertificationTypes, "Id", "Name", equipmentCertificationType.CertificationTypeId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName", equipmentCertificationType.EquipmentId);
            return View(equipmentCertificationType);
        }

        // GET: EquipmentCertificationType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentCertificationType = await _context.EquipmentCertificationTypes.FindAsync(id);
            if (equipmentCertificationType == null)
            {
                return NotFound();
            }
            ViewData["CertificationTypeId"] = new SelectList(_context.CertificationTypes, "Id", "Name", equipmentCertificationType.CertificationTypeId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName", equipmentCertificationType.EquipmentId);
            return View(equipmentCertificationType);
        }

        // POST: EquipmentCertificationType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EquipmentId,CertificationTypeId,Id")] EquipmentCertificationType equipmentCertificationType)
        {
            if (id != equipmentCertificationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentCertificationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentCertificationTypeExists(equipmentCertificationType.Id))
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
            ViewData["CertificationTypeId"] = new SelectList(_context.CertificationTypes, "Id", "Name", equipmentCertificationType.CertificationTypeId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName", equipmentCertificationType.EquipmentId);
            return View(equipmentCertificationType);
        }

        // GET: EquipmentCertificationType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentCertificationType = await _context.EquipmentCertificationTypes
                .Include(e => e.CertificationType)
                .Include(e => e.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentCertificationType == null)
            {
                return NotFound();
            }

            return View(equipmentCertificationType);
        }

        // POST: EquipmentCertificationType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var equipmentCertificationType = await _context.EquipmentCertificationTypes.FindAsync(id);
            if (equipmentCertificationType != null)
            {
                _context.EquipmentCertificationTypes.Remove(equipmentCertificationType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentCertificationTypeExists(Guid id)
        {
            return _context.EquipmentCertificationTypes.Any(e => e.Id == id);
        }
    }
}
