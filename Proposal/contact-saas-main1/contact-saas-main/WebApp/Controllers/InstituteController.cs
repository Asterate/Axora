using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Entities;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class InstituteController : Controller
    {
        private readonly AppDbContext _context;

        public InstituteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Institute
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Institutes.Include(i => i.InstituteType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Institute/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes
                .Include(i => i.InstituteType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }

        // GET: Institute/Create
        public IActionResult Create()
        {
            ViewData["InstituteTypeId"] = new SelectList(_context.InstituteTypes, "Id", "Name");
            return View();
        }

        // POST: Institute/Create
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            // Get InstituteTypeId from the form
            var typeIdStr = form["InstituteTypeId"].ToString();
            var typeId = Guid.Empty;
            
            // Try parsing the GUID
            if (!string.IsNullOrEmpty(typeIdStr))
            {
                Guid.TryParse(typeIdStr, out typeId);
            }
            
            // Simple validation: get values from form
            var name = form["InstituteName"].ToString();
            var country = form["InstituteCountry"].ToString();
            var address = form["InstituteAddress"].ToString();
            var phone = form["InstitutePhoneNumber"].ToString();
            var active = form["Active"].ToString().Contains("true");
            
            // Check required fields
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phone))
            {
                ViewBag.ErrorMessage = "All fields are required. Please fill all fields.";
            }
            else if (typeId == Guid.Empty)
            {
                ViewBag.ErrorMessage = $"Please select an Institute Type. Received: [{typeIdStr}]";
            }
            else
            {
                try
                {
                    var institute = new Institute
                    {
                        Id = Guid.NewGuid(),
                        InstituteName = name,
                        InstituteCountry = country,
                        InstituteAddress = address,
                        InstitutePhoneNumber = phone,
                        Active = active,
                        InstituteTypeId = typeId,
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.Add(institute);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error saving: " + ex.Message;
                }
            }
            
            ViewData["InstituteTypeId"] = new SelectList(_context.InstituteTypes, "Id", "Name");
            return View();
        }

        // GET: Institute/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes.FindAsync(id);
            if (institute == null)
            {
                return NotFound();
            }
            ViewData["InstituteTypeId"] = new SelectList(_context.InstituteTypes, "Id", "Name", institute.InstituteTypeId);
            return View(institute);
        }

        // POST: Institute/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InstituteName,InstituteCountry,InstituteAddress,InstitutePhoneNumber,CreatedAt,UpdatedAt,DeletedAt,Active,InstituteTypeId,Id")] Institute institute)
        {
            if (id != institute.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteExists(institute.Id))
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
            ViewData["InstituteTypeId"] = new SelectList(_context.InstituteTypes, "Id", "Name", institute.InstituteTypeId);
            return View(institute);
        }

        // GET: Institute/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes
                .Include(i => i.InstituteType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }

        // POST: Institute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var institute = await _context.Institutes.FindAsync(id);
            if (institute != null)
            {
                _context.Institutes.Remove(institute);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteExists(Guid id)
        {
            return _context.Institutes.Any(e => e.Id == id);
        }
    }
}
