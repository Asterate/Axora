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
    public class InstituteProjectController : Controller
    {
        private readonly AppDbContext _context;

        public InstituteProjectController(AppDbContext context)
        {
            _context = context;
        }

        // GET: InstituteProject
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.InstituteProjects.Include(i => i.Institute).Include(i => i.Project);
            return View(await appDbContext.ToListAsync());
        }

        // GET: InstituteProject/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteProject = await _context.InstituteProjects
                .Include(i => i.Institute)
                .Include(i => i.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instituteProject == null)
            {
                return NotFound();
            }

            return View(instituteProject);
        }

        // GET: InstituteProject/Create
        public IActionResult Create()
        {
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName");
            return View();
        }

        // POST: InstituteProject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedAt,UpdatedAt,DeletedAt,InstituteId,ProjectId,Id")] InstituteProject instituteProject)
        {
            if (ModelState.IsValid)
            {
                instituteProject.Id = Guid.NewGuid();
                _context.Add(instituteProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress", instituteProject.InstituteId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName", instituteProject.ProjectId);
            return View(instituteProject);
        }

        // GET: InstituteProject/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteProject = await _context.InstituteProjects.FindAsync(id);
            if (instituteProject == null)
            {
                return NotFound();
            }
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress", instituteProject.InstituteId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName", instituteProject.ProjectId);
            return View(instituteProject);
        }

        // POST: InstituteProject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CreatedAt,UpdatedAt,DeletedAt,InstituteId,ProjectId,Id")] InstituteProject instituteProject)
        {
            if (id != instituteProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituteProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteProjectExists(instituteProject.Id))
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
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress", instituteProject.InstituteId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName", instituteProject.ProjectId);
            return View(instituteProject);
        }

        // GET: InstituteProject/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteProject = await _context.InstituteProjects
                .Include(i => i.Institute)
                .Include(i => i.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instituteProject == null)
            {
                return NotFound();
            }

            return View(instituteProject);
        }

        // POST: InstituteProject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var instituteProject = await _context.InstituteProjects.FindAsync(id);
            if (instituteProject != null)
            {
                _context.InstituteProjects.Remove(instituteProject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteProjectExists(Guid id)
        {
            return _context.InstituteProjects.Any(e => e.Id == id);
        }
    }
}
