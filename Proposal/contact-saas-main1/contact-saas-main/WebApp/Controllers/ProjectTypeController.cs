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
    public class ProjectTypeController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProjectType
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectTypes.ToListAsync());
        }

        // GET: ProjectType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // GET: ProjectType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var name = new LangStr();
                name.SetTranslation(viewModel.NameEn, "en");
                name.SetTranslation(viewModel.NameEt, "et");
                
                var description = new LangStr();
                description.SetTranslation(viewModel.DescriptionEn ?? string.Empty, "en");
                description.SetTranslation(viewModel.DescriptionEt ?? string.Empty, "et");
                
                var projectType = new ProjectType
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description
                };
                _context.Add(projectType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "LookupData");
            }
            return View(viewModel);
        }

        // GET: ProjectType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectTypes.FindAsync(id);
            if (projectType == null)
            {
                return NotFound();
            }
            
            // Convert entity to ViewModel for display
            var viewModel = new ProjectTypeViewModel
            {
                Id = projectType.Id,
                NameEn = projectType.Name.Translate("en") ?? string.Empty,
                NameEt = projectType.Name.Translate("et") ?? string.Empty,
                DescriptionEn = projectType.Description?.Translate("en"),
                DescriptionEt = projectType.Description?.Translate("et")
            };
            return View(viewModel);
        }

        // POST: ProjectType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProjectTypeViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var projectType = await _context.ProjectTypes.FindAsync(id);
                    if (projectType == null)
                    {
                        return NotFound();
                    }
                    projectType.Name.SetTranslation(viewModel.NameEn, "en");
                    projectType.Name.SetTranslation(viewModel.NameEt, "et");
                    
                    if (projectType.Description == null)
                    {
                        projectType.Description = new LangStr();
                    }
                    projectType.Description.SetTranslation(viewModel.DescriptionEn ?? string.Empty, "en");
                    projectType.Description.SetTranslation(viewModel.DescriptionEt ?? string.Empty, "et");
                    
                    _context.Update(projectType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTypeExists(viewModel.Id))
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
            return View(viewModel);
        }

        // GET: ProjectType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _context.ProjectTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // POST: ProjectType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var projectType = await _context.ProjectTypes.FindAsync(id);
            if (projectType != null)
            {
                _context.ProjectTypes.Remove(projectType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "LookupData");
        }

        private bool ProjectTypeExists(Guid id)
        {
            return _context.ProjectTypes.Any(e => e.Id == id);
        }
    }
}
