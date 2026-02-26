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
    public class InstituteUserController : Controller
    {
        private readonly AppDbContext _context;

        public InstituteUserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: InstituteUser
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.InstituteUsers.Include(i => i.Institute);
            return View(await appDbContext.ToListAsync());
        }

        // GET: InstituteUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteUser = await _context.InstituteUsers
                .Include(i => i.Institute)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instituteUser == null)
            {
                return NotFound();
            }

            return View(instituteUser);
        }

        // GET: InstituteUser/Create
        public IActionResult Create()
        {
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress");
            return View();
        }

        // POST: InstituteUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstituteId,Role,Id")] InstituteUser instituteUser)
        {
            if (ModelState.IsValid)
            {
                instituteUser.Id = Guid.NewGuid();
                _context.Add(instituteUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress", instituteUser.InstituteId);
            return View(instituteUser);
        }

        // GET: InstituteUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteUser = await _context.InstituteUsers.FindAsync(id);
            if (instituteUser == null)
            {
                return NotFound();
            }
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress", instituteUser.InstituteId);
            return View(instituteUser);
        }

        // POST: InstituteUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InstituteId,Role,Id")] InstituteUser instituteUser)
        {
            if (id != instituteUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituteUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteUserExists(instituteUser.Id))
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
            ViewData["InstituteId"] = new SelectList(_context.Institutes, "Id", "InstituteAddress", instituteUser.InstituteId);
            return View(instituteUser);
        }

        // GET: InstituteUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteUser = await _context.InstituteUsers
                .Include(i => i.Institute)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instituteUser == null)
            {
                return NotFound();
            }

            return View(instituteUser);
        }

        // POST: InstituteUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var instituteUser = await _context.InstituteUsers.FindAsync(id);
            if (instituteUser != null)
            {
                _context.InstituteUsers.Remove(instituteUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteUserExists(Guid id)
        {
            return _context.InstituteUsers.Any(e => e.Id == id);
        }
    }
}
