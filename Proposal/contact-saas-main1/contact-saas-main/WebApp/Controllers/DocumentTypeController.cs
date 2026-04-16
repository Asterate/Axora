using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "admin")]
    public class DocumentTypeController : Controller
    {
        private readonly AppDbContext _context;

        public DocumentTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DocumentType
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocumentTypes.ToListAsync());
        }

        // GET: DocumentType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentType = await _context.DocumentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentType == null)
            {
                return NotFound();
            }

            return View("Index", "LookupData");
        }

        // GET: DocumentType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InstituteType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var name = new LangStr();
                name.SetTranslation(viewModel.NameEn, "en");
                name.SetTranslation(viewModel.NameEt, "et");
        
                var description = new LangStr();
                description.SetTranslation(viewModel.DescriptionEn ?? string.Empty, "en");
                description.SetTranslation(viewModel.DescriptionEt ?? string.Empty, "et");
        
                var documentType = new DocumentType
                {
                    Name = name,
                    Description = description
                };
        
                _context.Add(documentType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "LookupData");
            }
            return View(viewModel);
        }


        // GET: DocumentType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentType = await _context.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return NotFound();
            }
            return View(documentType);
        }

        // POST: DocumentType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] DocumentType documentType)
        {
            if (id != documentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentTypeExists(documentType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "LookupData");
            }
            return View("Index", "LookupData");
        }

        // GET: DocumentType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentType = await _context.DocumentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentType == null)
            {
                return NotFound();
            }

            return View(documentType);
        }

        // POST: DocumentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var documentType = await _context.DocumentTypes.FindAsync(id);
            if (documentType != null)
            {
                _context.DocumentTypes.Remove(documentType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "LookupData");
        }

        private bool DocumentTypeExists(Guid id)
        {
            return _context.DocumentTypes.Any(e => e.Id == id);
        }
    }
}
