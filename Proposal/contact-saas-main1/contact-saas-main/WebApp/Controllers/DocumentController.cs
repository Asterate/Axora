using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly DocumentService _document;

        public DocumentController(DocumentService documentService)
        {
            _document = documentService;
        }

        // GET: Document
        public async Task<IActionResult> Index()
        {
            var documents = _document.GetAllAsync();
            return View(documents);
        }

        // GET: Document/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _document.GetByIdAsync(id.Value);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Document/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Document/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentName,CreatedAt,UpdatedAt,DeletedAt,FilePath,DocumentTypeId,Id")] Document document)
        {
            if (ModelState.IsValid)
            {
                await _document.CreateAsync(new CreateDocumentRequest
                {
                    DocumentName = document.DocumentName,
                    FilePath = document.FilePath,
                    DocumentTypeId = document.DocumentTypeId
                });
            }
            return View(document);
        }

        // GET: Document/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                TempData["Error"] = "Document not found";
                return NotFound();
            }

            var document = await _document.GetByIdAsync(id.Value);
            if (document == null)
            {
                TempData["Error"] = "Document not found";
                return NotFound();
            }

            return View(document);
        }

        // POST: Document/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DocumentName,CreatedAt,UpdatedAt,FilePath,DocumentTypeId,Id")] Document document)
        {
            if (id != document.Id)
            {
                TempData["Error"] = "Document not found";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var update = new UpdateDocumentRequest(document);
                    await _document.UpdateAsync(id, update);
                    TempData["Success"] = "Document edited";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DocumentExists(document.Id))
                    {
                        TempData["Error"] = "Document not found";
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(document);
        }

        // GET: Document/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                TempData["Error"] = "Document not found";
                return NotFound();
            }

            var document = await _document.GetByIdAsync(id.Value);
            if (document == null)
            {
                TempData["Error"] = "Document not found";
                return NotFound();
            }
            
            return View(document);
        }

        // POST: Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var document = await _document.GetByIdAsync(id);
            if (document != null)
            {
                TempData["Error"] = "Document not found";
                await _document.DeleteAsync(id);
            }
            TempData["Success"] = "Document deleted";
            return RedirectToAction(nameof(Index));
        }
        private async Task<bool> DocumentExists(Guid id)
        {
            return await _document.GetByIdAsync(id) != null;
        }
    }
}
