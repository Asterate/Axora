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
    public class InstituteTypeController : Controller
    {
        private readonly AppDbContext _context;

        public InstituteTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: InstituteType
        public async Task<IActionResult> Index()
        {
            return View(await _context.InstituteTypes.ToListAsync());
        }

        // GET: InstituteType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteType = await _context.InstituteTypes
                .FirstOrDefaultAsync(m => m.Id == id);
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
                name.SetTranslation(viewModel.NameEn, "en");
                name.SetTranslation(viewModel.NameEt, "et");
                
                var description = new LangStr();
                description.SetTranslation(viewModel.DescriptionEn ?? string.Empty, "en");
                description.SetTranslation(viewModel.DescriptionEt ?? string.Empty, "et");
                
                var instituteType = new InstituteType
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description
                };
                _context.Add(instituteType);
                await _context.SaveChangesAsync();
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

            var instituteType = await _context.InstituteTypes.FindAsync(id);
            if (instituteType == null)
            {
                return NotFound();
            }
            
            // Convert to ViewModel for display
            var viewModel = new InstituteTypeViewModel
            {
                Id = instituteType.Id,
                NameEn = instituteType.Name.Translate("en") ?? string.Empty,
                NameEt = instituteType.Name.Translate("et") ?? string.Empty,
                DescriptionEn = instituteType.Description?.Translate("en"),
                DescriptionEt = instituteType.Description?.Translate("et")
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
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var instituteType = await _context.InstituteTypes.FindAsync(id);
                    if (instituteType == null)
                    {
                        return NotFound();
                    }
                    
                    // Update translations
                    instituteType.Name.SetTranslation(viewModel.NameEn, "en");
                    instituteType.Name.SetTranslation(viewModel.NameEt, "et");
                    
                    if (instituteType.Description == null)
                    {
                        instituteType.Description = new LangStr();
                    }
                    instituteType.Description.SetTranslation(viewModel.DescriptionEn ?? string.Empty, "en");
                    instituteType.Description.SetTranslation(viewModel.DescriptionEt ?? string.Empty, "et");
                    
                    _context.Update(instituteType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteTypeExists(viewModel.Id))
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

        // GET: InstituteType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteType = await _context.InstituteTypes
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var instituteType = await _context.InstituteTypes.FindAsync(id);
            if (instituteType != null)
            {
                _context.InstituteTypes.Remove(instituteType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "LookupData");
        }

        private bool InstituteTypeExists(Guid id)
        {
            return _context.InstituteTypes.Any(e => e.Id == id);
        }
    }
}
