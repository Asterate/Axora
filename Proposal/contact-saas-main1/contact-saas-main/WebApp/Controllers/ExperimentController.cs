using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class ExperimentController : Controller
    {
        private readonly ExperimentService _experiment;

        public ExperimentController(ExperimentService experiementService)
        {
            _experiment = experiementService;
        }

        // GET: Experiment
        public async Task<IActionResult> Index()
        {
            var experiments = _experiment.GetAllAsync();
            return View(experiments);
        }

        // GET: Experiment/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _experiment.GetByIdAsync(id.Value);
            if (experiment == null)
            {
                return NotFound();
            }

            return View(experiment);
        }

        // GET: Experiment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Experiment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExperimentName,ExperimentNotes,CreatedAt,UpdatedAt,DeletedAt,ExperimentTypeId,ProjectId,InstituteUserId,Id")] Experiment experiment)
        {
            if (ModelState.IsValid)
            {
                await _experiment.CreateAsync(new CreateExperimentRequest
                {
                    ExperimentName = experiment.ExperimentName,
                    ExperimentNotes = experiment.ExperimentNotes,
                    ExperimentTypeId = experiment.ExperimentTypeId,
                    ProjectId = experiment.ProjectId,
                    InstituteUserId = experiment.InstituteUserId
                });
            }
           
            return View(experiment);
        }

        // GET: Experiment/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _experiment.GetByIdAsync(id.Value);
            if (experiment == null)
            {
                return NotFound();
            }
            
            return View(experiment);
        }

        // POST: Experiment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ExperimentName,ExperimentNotes,CreatedAt,UpdatedAt,DeletedAt,ExperimentTypeId,ProjectId,InstituteUserId,Id")] Experiment experiment)
        {
            if (id != experiment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var update = new UpdateExperimentRequest(experiment);
                    await _experiment.UpdateAsync(id, update);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ExperimentExists(experiment.Id))
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
            return View(experiment);
        }

        // GET: Experiment/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _experiment.GetByIdAsync(id.Value);
            if (experiment == null)
            {
                return NotFound();
            }

            return View(experiment);
        }

        // POST: Experiment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var experiment = await _experiment.GetByIdAsync(id);
            if (experiment != null)
            {
                await _experiment.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ExperimentExists(Guid id)
        {
            return await _experiment.GetByIdAsync(id) != null;
        }
    }
}
