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
    public class DocumentResultController : Controller
    {
        private readonly AppDbContext _context;

        public DocumentResultController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DocumentResult
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DocumentResults.Include(d => d.Document).Include(d => d.Result);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DocumentResult/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentResult = await _context.DocumentResults
                .Include(d => d.Document)
                .Include(d => d.Result)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentResult == null)
            {
                return NotFound();
            }

            return View(documentResult);
        }

        // GET: DocumentResult/Create
        public IActionResult Create()
        {
            ViewData["DocumentId"] = new SelectList(_context.Documents, "Id", "DocumentName");
            ViewData["ResultId"] = new SelectList(_context.Results, "Id", "ResultDescription");
            return View();
        }

        // POST: DocumentResult/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedAt,UpdatedAt,DeletedAt,DocumentId,ResultId,Id")] DocumentResult documentResult)
        {
            if (ModelState.IsValid)
            {
                documentResult.Id = Guid.NewGuid();
                _context.Add(documentResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocumentId"] = new SelectList(_context.Documents, "Id", "DocumentName", documentResult.DocumentId);
            ViewData["ResultId"] = new SelectList(_context.Results, "Id", "ResultDescription", documentResult.ResultId);
            return View(documentResult);
        }

        // GET: DocumentResult/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentResult = await _context.DocumentResults.FindAsync(id);
            if (documentResult == null)
            {
                return NotFound();
            }
            ViewData["DocumentId"] = new SelectList(_context.Documents, "Id", "DocumentName", documentResult.DocumentId);
            ViewData["ResultId"] = new SelectList(_context.Results, "Id", "ResultDescription", documentResult.ResultId);
            return View(documentResult);
        }

        // POST: DocumentResult/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CreatedAt,UpdatedAt,DeletedAt,DocumentId,ResultId,Id")] DocumentResult documentResult)
        {
            if (id != documentResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentResultExists(documentResult.Id))
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
            ViewData["DocumentId"] = new SelectList(_context.Documents, "Id", "DocumentName", documentResult.DocumentId);
            ViewData["ResultId"] = new SelectList(_context.Results, "Id", "ResultDescription", documentResult.ResultId);
            return View(documentResult);
        }

        // GET: DocumentResult/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentResult = await _context.DocumentResults
                .Include(d => d.Document)
                .Include(d => d.Result)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentResult == null)
            {
                return NotFound();
            }

            return View(documentResult);
        }

        // POST: DocumentResult/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var documentResult = await _context.DocumentResults.FindAsync(id);
            if (documentResult != null)
            {
                _context.DocumentResults.Remove(documentResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentResultExists(Guid id)
        {
            return _context.DocumentResults.Any(e => e.Id == id);
        }
    }
}
