using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Entities;
using App.Modules.Lab.Application.DTO;
using App.Modules.Lab.Application.Mappers;
using App.Modules.Lab.Application.Services;
using App.Modules.Lab.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "admin")]
    public class InstituteLabController : Controller
    {
        private readonly InstituteLabService _instituteLabService;

        public InstituteLabController(InstituteLabService instituteLabService)
        {
            _instituteLabService = instituteLabService;
        }

        // GET: InstituteLab
        public async Task<IActionResult> Index()
        {
            var instituteLabs = await _instituteLabService.GetAllAsync();
            return View(instituteLabs);
        }

        // GET: InstituteLab/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteLab = await _instituteLabService.GetByIdAsync(id.Value);
            if (instituteLab == null)
            {
                return NotFound();
            }

            return View(instituteLab);
        }

        // GET: InstituteLab/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: InstituteLab/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedAt,UpdatedAt,DeletedAt,InstituteId,LabId,Id")] InstituteLab instituteLab)
        {
            if (ModelState.IsValid)
            {
                var newInstituteLab = new CreateInstituteLabRequest()
                {
                    InstituteId = instituteLab.InstituteId,
                    LabId = instituteLab.LabId,
                };
                await _instituteLabService.CreateAsync(newInstituteLab);
                return RedirectToAction(nameof(Index));
            }
            return View(instituteLab);
        }

        // GET: InstituteLab/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteLab = await _instituteLabService.GetByIdAsync(id.Value);
            if (instituteLab == null)
            {
                return NotFound();
            }
            return View(instituteLab);
        }

        // POST: InstituteLab/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CreatedAt,UpdatedAt,DeletedAt,InstituteId,LabId,Id")] InstituteLab instituteLab)
        {
            if (id != instituteLab.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var instituteLabInstitute = InstituteLabMapper.ToUpdateRequest(instituteLab);
                    await _instituteLabService.UpdateAsync(id, instituteLabInstitute);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InstituteLabExists(instituteLab.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instituteLab);
        }

        // GET: InstituteLab/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteLab = await _instituteLabService.GetByIdAsync(id.Value);
            if (instituteLab == null)
            {
                return NotFound();
            }

            return View(instituteLab);
        }

        // POST: InstituteLab/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var instituteLab = await _instituteLabService.GetByIdAsync(id);
            if (instituteLab != null)
            {
                await _instituteLabService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> InstituteLabExists(Guid id)
        {
            return await _instituteLabService.GetByIdAsync(id) != null;
        }
    }
}
