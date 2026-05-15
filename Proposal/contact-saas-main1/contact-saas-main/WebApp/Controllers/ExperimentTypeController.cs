using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Entities;
using App.Modules.Experiment.Application.Mapper;
using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Services;
using App.Modules.Project.Domain;
using App.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "admin")]
    public class ExperimentTypeController : Controller
    {
        private readonly ExperimentTypeService _experimentTypeService;

        public ExperimentTypeController(ExperimentTypeService experimentTypeService)
        {
            _experimentTypeService = experimentTypeService;
        }

        // GET: ExperimentType
        public async Task<IActionResult> Index()
        {
            return View(await _experimentTypeService.GetAllAsync());
        }

        // GET: ExperimentType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentType = await _experimentTypeService.GetByIdAsync(id.Value);
            if (experimentType == null)
            {
                return NotFound();
            }

            return View(experimentType);
        }

        // GET: ExperimentType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InstituteType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExperimentTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var name = new LangStr();
                name.SetTranslation(viewModel.NameEn, "en");
                name.SetTranslation(viewModel.NameEt, "et");
                
                var description = new LangStr();
                description.SetTranslation(viewModel.DescriptionEn ?? string.Empty, "en");
                description.SetTranslation(viewModel.DescriptionEt ?? string.Empty, "et");
                
                var experimentType = new CreateExperimentTypeRequest()
                {
                    NameEn = viewModel.NameEn,
                    NameEt = viewModel.NameEt,
                    DescriptionEn = viewModel.DescriptionEn,
                    DescriptionEt = viewModel.DescriptionEt
                };
                
                await _experimentTypeService.CreateAsync(experimentType);
                return RedirectToAction("Index", "LookupData");
            }
            return View(viewModel);
        }

        // GET: ExperimentType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentType = await _experimentTypeService.GetByIdAsync(id.Value);
            if (experimentType == null)
            {
                return NotFound();
            }
            return View(experimentType);
        }

        // POST: ExperimentType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] ExperimentType experimentType)
        {
            if (id != experimentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var update = ExperimentTypeMapper.ToUpdateRequest(experimentType);
                    await _experimentTypeService.UpdateAsync(id, update);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ExperimentTypeExists(experimentType.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction("Index", "LookupData");
            }
            return View(experimentType);
        }

        // GET: ExperimentType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentType = await _experimentTypeService.GetByIdAsync(id.Value);
            if (experimentType == null)
            {
                return NotFound();
            }

            return View(experimentType);
        }

        // POST: ExperimentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var experimentType = await _experimentTypeService.GetByIdAsync(id);
            if (experimentType != null)
            {
                await _experimentTypeService.DeleteAsync(id);
            }

            return RedirectToAction("Index", "LookupData");
        }

        private async Task<bool> ExperimentTypeExists(Guid id)
        {
            return await _experimentTypeService.GetByIdAsync(id) != null;
        }
    }
}
