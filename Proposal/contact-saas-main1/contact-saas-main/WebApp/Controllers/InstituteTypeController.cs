using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Entities;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Mappers;
using App.Modules.Project.Application.Services;
using App.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "admin")]
    public class InstituteTypeController : Controller
    {
        private readonly IInstituteTypeService _instituteTypeService;

        public InstituteTypeController(IInstituteTypeService instituteTypeService)
        {
            _instituteTypeService = instituteTypeService;
        }

        // GET: InstituteType
        public async Task<IActionResult> Index()
        {
            return View(await _instituteTypeService.GetAllAsync());
        }

        // GET: InstituteType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteType = await _instituteTypeService.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Create(InstituteTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var name = new LangStr();
                name.SetTranslation(viewModel.InstituteTypesRequest.NameEn, "en");
                name.SetTranslation(viewModel.InstituteTypesRequest.NameEt, "et");
                
                var description = new LangStr();
                description.SetTranslation(viewModel.InstituteTypesRequest.DescriptionEn ?? string.Empty, "en");
                description.SetTranslation(viewModel.InstituteTypesRequest.DescriptionEt ?? string.Empty, "et");
                
                var experimentType = new SaveInstituteTypeRequest()
                {
                    NameEn = viewModel.InstituteTypesRequest.NameEn,
                    NameEt = viewModel.InstituteTypesRequest.NameEt,
                    DescriptionEn = viewModel.InstituteTypesRequest.DescriptionEn,
                    DescriptionEt = viewModel.InstituteTypesRequest.DescriptionEt
                };
                
                await _instituteTypeService.CreateAsync(experimentType);
                return RedirectToAction("Index", "LookupData");
            }
            return View(viewModel);
        }

        // GET: InstituteType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteType = await _instituteTypeService.GetByIdAsync(id.Value);
            if (instituteType == null)
            {
                return NotFound();
            }
            var viewModel = new InstituteTypeViewModel
            {
                InstituteTypesResponse = instituteType 
            };
            return View(viewModel);
        }

        // POST: InstituteType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, InstituteTypeViewModel viewModel)
        {
            if (id != viewModel.InstituteTypesResponse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _instituteTypeService.UpdateAsync(id, viewModel.InstituteTypesRequest);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InstituteTypeExists(viewModel.InstituteTypesResponse.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction("Index", "LookupData");
            }
            return View(viewModel);
        }

        // GET: InstituteType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteType = await _instituteTypeService.GetByIdAsync(id.Value);
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
            var instituteType = await _instituteTypeService.GetByIdAsync(id);
            if (instituteType != null)
            {
                await _instituteTypeService.DeleteAsync(id);
            }

            return RedirectToAction("Index", "LookupData");
        }

        private async Task<bool> InstituteTypeExists(Guid id)
        {
            return await _instituteTypeService.GetByIdAsync(id) != null;
        }
    }
}
