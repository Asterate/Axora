using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly DocumentService _document;
        private readonly DocumentTypeService _documentTypeService;

        public DocumentController(DocumentService documentService, DocumentTypeService documentTypeService)
        {
            _document = documentService;
            _documentTypeService = documentTypeService;
        }

        // GET: Document
        public async Task<IActionResult> Index()
        {
            var documents = await _document.GetAllAsync();
            return View("~/Views/DocumentDashboard/Index.cshtml",documents);
        }

        // GET: Document/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var document = await _document.GetByIdAsync(id);
            if (document == null) return NotFound();

            var response = new DocumentResponse
            {
                Id = document.Id,
                DocumentName = document.DocumentName,
                Description = document.Description,
                DocumentType = document.DocumentType,
                FilePath = document.FilePath,
                DocumentTypeId = document.DocumentTypeId
            };

            return View(response);
        }

        // GET: Document/Create
        public async Task<IActionResult> Create()
        {
            var model = new DocumentationViewModel
            {
                DocumentTypes = await _documentTypeService.GetActivesAsync()
            };
            return View(model);
        }

        // POST: Document/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentationViewModel model)
        {
            try
            {
                await _document.CreateAsync(model.Request);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                model.DocumentTypes = await _documentTypeService.GetActivesAsync();
                return View(model);
            }
        }

        // GET: Document/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var document = await _document.GetByIdAsync(id);
            if (document == null) return NotFound();

            var model = new DocumentationViewModel
            {
                Request = new UpdateDocumentRequest
                {
                    Id = document.Id,
                    DocumentName = document.DocumentName,
                    Description = document.Description,
                    FilePath = document.FilePath,
                    DocumentTypeId = document.DocumentTypeId,
                },
                DocumentTypes = await _document.GetActivesAsync()
            };

            return View(model);
        }

        // POST: Document/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DocumentationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _document.UpdateAsync(id, model.Request);
            return RedirectToAction(nameof(Index));
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
                TempData["Success"] = "Document found";
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
