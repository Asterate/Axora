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
    public class EquipmentLabController : Controller
    {
        private readonly AppDbContext _context;

        public EquipmentLabController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EquipmentLab
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EquipmentLabs.Include(e => e.Equipment).Include(e => e.Lab);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EquipmentLab/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentLab = await _context.EquipmentLabs
                .Include(e => e.Equipment)
                .Include(e => e.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentLab == null)
            {
                return NotFound();
            }

            return View(equipmentLab);
        }

        // GET: EquipmentLab/Create
        public IActionResult Create()
        {
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName");
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress");
            return View();
        }

        // POST: EquipmentLab/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,CreatedAt,UpdatedAt,DeletedAt,LabId,EquipmentId,Id")] EquipmentLab equipmentLab)
        {
            if (ModelState.IsValid)
            {
                equipmentLab.Id = Guid.NewGuid();
                _context.Add(equipmentLab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName", equipmentLab.EquipmentId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", equipmentLab.LabId);
            return View(equipmentLab);
        }

        // GET: EquipmentLab/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentLab = await _context.EquipmentLabs.FindAsync(id);
            if (equipmentLab == null)
            {
                return NotFound();
            }
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName", equipmentLab.EquipmentId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", equipmentLab.LabId);
            return View(equipmentLab);
        }

        // POST: EquipmentLab/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Quantity,CreatedAt,UpdatedAt,DeletedAt,LabId,EquipmentId,Id")] EquipmentLab equipmentLab)
        {
            if (id != equipmentLab.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentLab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentLabExists(equipmentLab.Id))
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
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "EquipmentName", equipmentLab.EquipmentId);
            ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabAddress", equipmentLab.LabId);
            return View(equipmentLab);
        }

        // GET: EquipmentLab/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentLab = await _context.EquipmentLabs
                .Include(e => e.Equipment)
                .Include(e => e.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentLab == null)
            {
                return NotFound();
            }

            return View(equipmentLab);
        }

        // POST: EquipmentLab/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var equipmentLab = await _context.EquipmentLabs.FindAsync(id);
            if (equipmentLab != null)
            {
                _context.EquipmentLabs.Remove(equipmentLab);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentLabExists(Guid id)
        {
            return _context.EquipmentLabs.Any(e => e.Id == id);
        }
    }
}
