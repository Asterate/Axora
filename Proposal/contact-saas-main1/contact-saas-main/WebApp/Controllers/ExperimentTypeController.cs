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
    public class ExperimentTypeController : Controller
    {
        private readonly AppDbContext _context;

        public ExperimentTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExperimentType
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExperimentTypes.ToListAsync());
        }

        // GET: ExperimentType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentType = await _context.ExperimentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
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
                
                var experimentType = new ExperimentType()
                {
                    Name = name,
                    Description = description
                };
                
                _context.Add(experimentType);
                await _context.SaveChangesAsync();
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

            var experimentType = await _context.ExperimentTypes.FindAsync(id);
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
                    _context.Update(experimentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperimentTypeExists(experimentType.Id))
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
            return View(experimentType);
        }

        // GET: ExperimentType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentType = await _context.ExperimentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var experimentType = await _context.ExperimentTypes.FindAsync(id);
            if (experimentType != null)
            {
                _context.ExperimentTypes.Remove(experimentType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "LookupData");
        }

        private bool ExperimentTypeExists(Guid id)
        {
            return _context.ExperimentTypes.Any(e => e.Id == id);
        }
    }
}
