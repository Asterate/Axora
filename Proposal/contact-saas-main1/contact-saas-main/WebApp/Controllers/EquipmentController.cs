using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Entities;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class EquipmentController : Controller
    {
        private readonly AppDbContext _context;

        public EquipmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Equipment
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Equipments.Include(e => e.EquipmentType);
            var items = await appDbContext.ToListAsync();
            return View(items.Select(EquipmentViewModel.FromEntity).ToList());
        }

        // GET: Equipment/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .Include(e => e.EquipmentType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(EquipmentViewModel.FromEntity(equipment));
        }

        // GET: Equipment/Create
        public IActionResult Create()
        {
            ViewData["EquipmentTypeId"] = new SelectList(_context.EquipmentTypes, "Id", "EquipmentTypeName");
            return View();
        }

        // POST: Equipment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var equipment = viewModel.ToEntity();
                equipment.Id = Guid.NewGuid();
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentTypeId"] = new SelectList(_context.EquipmentTypes, "Id", "EquipmentTypeName", viewModel.EquipmentTypeId);
            return View(viewModel);
        }

        // GET: Equipment/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }
            ViewData["EquipmentTypeId"] = new SelectList(_context.EquipmentTypes, "Id", "EquipmentTypeName", equipment.EquipmentTypeId);
            return View(EquipmentViewModel.FromEntity(equipment));
        }

        // POST: Equipment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EquipmentViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var equipment = viewModel.ToEntity();
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(viewModel.Id))
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
            ViewData["EquipmentTypeId"] = new SelectList(_context.EquipmentTypes, "Id", "EquipmentTypeName", viewModel.EquipmentTypeId);
            return View(viewModel);
        }

        // GET: Equipment/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .Include(e => e.EquipmentType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(EquipmentViewModel.FromEntity(equipment));
        }

        // POST: Equipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment != null)
            {
                _context.Equipments.Remove(equipment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(Guid id)
        {
            return _context.Equipments.Any(e => e.Id == id);
        }
    }
}
