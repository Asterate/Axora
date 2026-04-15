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
    public class ReagentTypeController : Controller
    {
        private readonly AppDbContext _context;

        public ReagentTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReagentType
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReagentTypes.ToListAsync());
        }

        // GET: ReagentType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reagentType = await _context.ReagentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reagentType == null)
            {
                return NotFound();
            }

            return View(reagentType);
        }

        // GET: ReagentType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReagentType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReagentName,ReagentDescription,Category,DefaultStorage,HazardLevel,StandardConcentration,MaterialFilePath,IsHazardous,ColorCode,Id")] ReagentType reagentType)
        {
            if (ModelState.IsValid)
            {
                reagentType.Id = Guid.NewGuid();
                _context.Add(reagentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reagentType);
        }

        // GET: ReagentType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reagentType = await _context.ReagentTypes.FindAsync(id);
            if (reagentType == null)
            {
                return NotFound();
            }
            return View(reagentType);
        }

        // POST: ReagentType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ReagentName,ReagentDescription,Category,DefaultStorage,HazardLevel,StandardConcentration,MaterialFilePath,IsHazardous,ColorCode,Id")] ReagentType reagentType)
        {
            if (id != reagentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reagentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReagentTypeExists(reagentType.Id))
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
            return View(reagentType);
        }

        // GET: ReagentType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reagentType = await _context.ReagentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reagentType == null)
            {
                return NotFound();
            }

            return View(reagentType);
        }

        // POST: ReagentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reagentType = await _context.ReagentTypes.FindAsync(id);
            if (reagentType != null)
            {
                _context.ReagentTypes.Remove(reagentType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReagentTypeExists(Guid id)
        {
            return _context.ReagentTypes.Any(e => e.Id == id);
        }
    }
}
