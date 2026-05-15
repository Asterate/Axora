using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Entities;
using App.Modules.Institute.Application.Mapper;
using App.Modules.Project.Application.DTO;
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
        private readonly InstituteTypeService _instituteTypeService;

        public InstituteTypeController(InstituteTypeService instituteTypeService)
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
                name.SetTranslation(viewModel.InstituteTypes.NameEn ?? "??", "en");
                name.SetTranslation(viewModel.InstituteTypes.NameEt ?? "??", "et");
                
                var description = new LangStr();
                description.SetTranslation(viewModel.InstituteTypes.DescriptionEn ?? string.Empty, "en");
                description.SetTranslation(viewModel.InstituteTypes.DescriptionEt ?? string.Empty, "et");
                
                var instituteType = new CreateInstituteTypeRequest()
                {
                    NameEn = viewModel.InstituteTypes.NameEn,
                    NameEt = viewModel.InstituteTypes.NameEt,
                    DescriptionEn = viewModel.InstituteTypes.DescriptionEn,
                    DescriptionEt = viewModel.InstituteTypes.DescriptionEt
                };
                await _instituteTypeService.CreateAsync(instituteType);
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
                InstituteTypes = instituteType 
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
            if (id != viewModel.InstituteTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var instituteType = await _instituteTypeService.GetByIdAsync(id);
                    if (instituteType == null)
                    {
                        return NotFound();
                    }
                    
                    // Update translations
                    //instituteType.Name.SetTranslation(viewModel.NameEn, "en");
                    //instituteType.Name.SetTranslation(viewModel.NameEt, "et");
                    
                    /*if (instituteType.Description == null)
                    {
                        instituteType.Description = new LangStr();
                    }
                    instituteType.Description.SetTranslation(viewModel.DescriptionEn ?? string.Empty, "en");
                    instituteType.Description.SetTranslation(viewModel.DescriptionEt ?? string.Empty, "et");
                    */
                    var instituteTypeUpdate = InstituteTypeMapper.ToUpdateRequest(instituteType);
                    await _instituteTypeService.UpdateAsync(id, instituteTypeUpdate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InstituteTypeExists(viewModel.InstituteTypes.Id))
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
