using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Modules.Project.Application.Mappers;
using App.Modules.Project.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        // GET: Result
        public async Task<IActionResult> Index()
        {
            var results = await _resultService.GetAllAsync();
            return View(results);
        }

        // GET: Result/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _resultService.GetByIdAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Result/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Result/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultName,ResultDescription,MeasurementName,MeasurementValue,Unit,Notes,FilePath,CreatedAt,UpdatedAt,DeletedAt,ExperimentId,ExperimentTaskId,Id")] Result result)
        {
            if (ModelState.IsValid)
            {
                var create = new SaveResultRequest
                {
                    //issue
                    ResultNameEn = result.ResultName,
                    ExperimentId = result.ExperimentId,
                    ResultDescriptionEn  = result.ResultDescription,
                    MeasurementNameEn = result.MeasurementName,
                    MeasurementValue = result.MeasurementValue,
                    UnitEn =  result.Unit,
                    NotesEn = result.Notes,
                    FilePath = result.FilePath,
                    ProjectId = result.ProjectId
                };
                await _resultService.CreateAsync(create);
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        // GET: Result/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _resultService.GetByIdAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // POST: Result/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ResultName,ResultDescription,MeasurementName,MeasurementValue,Unit,Notes,FilePath,CreatedAt,UpdatedAt,DeletedAt,ExperimentId,ExperimentTaskId,Id")] Result result)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var update = ResultMapper.ToUpdateRequest(result);
                    await _resultService.UpdateAsync(id, update);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ResultExists(result.Id))
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
            return View(result);
        }

        // GET: Result/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _resultService.GetByIdAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _resultService.GetByIdAsync(id);
            if (result != null)
            {
               await _resultService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ResultExists(Guid id)
        {
            return await _resultService.GetByIdAsync(id) != null;
        }
    }
}
