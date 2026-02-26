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
    public class InstituteTypeController : Controller
    {
        private readonly AppDbContext _context;

        public InstituteTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: InstituteType
        public async Task<IActionResult> Index()
        {
            return View(await _context.InstituteTypes.ToListAsync());
        }

        // GET: InstituteType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteType = await _context.InstituteTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instituteType == null)
            {
                return NotFound();
            }

            return View(instituteType);
        }

        // GET: InstituteType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InstituteType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id")] InstituteType instituteType)
        {
            if (ModelState.IsValid)
            {
                instituteType.Id = Guid.NewGuid();
                _context.Add(instituteType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instituteType);
        }

        // GET: InstituteType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteType = await _context.InstituteTypes.FindAsync(id);
            if (instituteType == null)
            {
                return NotFound();
            }
            return View(instituteType);
        }

        // POST: InstituteType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Id")] InstituteType instituteType)
        {
            if (id != instituteType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituteType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteTypeExists(instituteType.Id))
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
            return View(instituteType);
        }

        // GET: InstituteType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteType = await _context.InstituteTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instituteType == null)
            {
                return NotFound();
            }

            return View(instituteType);
        }

        // POST: InstituteType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var instituteType = await _context.InstituteTypes.FindAsync(id);
            if (instituteType != null)
            {
                _context.InstituteTypes.Remove(instituteType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteTypeExists(Guid id)
        {
            return _context.InstituteTypes.Any(e => e.Id == id);
        }
    }
}
