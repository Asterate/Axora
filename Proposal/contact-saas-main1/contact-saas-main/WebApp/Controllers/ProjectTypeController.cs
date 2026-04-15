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
    public class ProjectTypeController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProjectType
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectTypes.ToListAsync());
        }

        // GET: ProjectType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // GET: ProjectType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id")] ProjectType projectType)
        {
            if (ModelState.IsValid)
            {
                projectType.Id = Guid.NewGuid();
                _context.Add(projectType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectType);
        }

        // GET: ProjectType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectTypes.FindAsync(id);
            if (projectType == null)
            {
                return NotFound();
            }
            return View(projectType);
        }

        // POST: ProjectType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Id")] ProjectType projectType)
        {
            if (id != projectType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTypeExists(projectType.Id))
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
            return View(projectType);
        }

        // GET: ProjectType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // POST: ProjectType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var projectType = await _context.ProjectTypes.FindAsync(id);
            if (projectType != null)
            {
                _context.ProjectTypes.Remove(projectType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTypeExists(Guid id)
        {
            return _context.ProjectTypes.Any(e => e.Id == id);
        }
    }
}
