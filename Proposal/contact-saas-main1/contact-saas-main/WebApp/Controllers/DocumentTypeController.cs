using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Modules.Project.Application.Services;
using App.Modules.Project.Domain;
using App.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "admin")]
    public class DocumentTypeController : Controller
    {
        private readonly IDocumentTypeService _documentType;

        public DocumentTypeController(IDocumentTypeService documentType)
        {
            _documentType = documentType;
        }

        // GET: DocumentType
        public async Task<IActionResult> Index()
        {
            return View(await _documentType.GetAllAsync());
        }

        // GET: DocumentType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentType = await _documentType.GetByIdAsync(id.Value);
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
        
                var documentType = new SaveDocumentTypeRequest
                {
                    NameEn = viewModel.NameEn,
                    NameEt = viewModel.NameEt,
                    DescriptionEn = viewModel.DescriptionEn,
                    DescriptionEt = viewModel.DescriptionEt
                };
        
                await _documentType.CreateAsync(documentType);
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

            var documentType = await _documentType.GetByIdAsync(id.Value);
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
                    var update = DocumentTypeMapper.ToUpdateRequest(documentType);
                    await _documentType.UpdateAsync(id, update);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DocumentTypeExists(documentType.Id))
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

            var documentType = await _documentType.GetByIdAsync(id.Value);
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
            var documentType = await _documentType.GetByIdAsync(id);
            if (documentType != null)
            {
                await _documentType.DeleteAsync(id);
            }

            return RedirectToAction("Index", "LookupData");
        }

        private async Task<bool> DocumentTypeExists(Guid id)
        {
            return await _documentType.GetByIdAsync(id) != null;
        }
    }
}
