using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class InstituteController : Controller
    {
        private readonly InstituteTypeService _instituteTypeService;
        private readonly InstituteService _instituteService;

        public InstituteController(InstituteTypeService instituteTypeService,
            InstituteService instituteService)
        {
            _instituteTypeService = instituteTypeService;
            _instituteService = instituteService;
        }

        // GET: Institute
        public async Task<IActionResult> Index()
        {
            var instituteTypes = _instituteTypeService.GetAllAsync();
            return View(instituteTypes);
        }

        // GET: Institute/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _instituteTypeService.GetByIdAsync(id.Value);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }

        // GET: Institute/Create
        public IActionResult Create()
        {
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
                    var institute = new CreateInstituteRequest()
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
                    await _instituteService.CreateAsync(institute);
                    return RedirectToAction("Index", "Establishments");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error saving: " + ex.Message;
                }
            }
            
            return RedirectToAction("Index", "Establishments");
        }

        // GET: Institute/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _instituteService.GetByIdAsync(id.Value);
            if (institute == null)
            {
                return NotFound();
            }
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
                    var update = new UpdateInstituteRequest(institute);
                    await _instituteService.UpdateAsync(id, update);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InstituteExists(institute.Id))
                    {
                        return NotFound();
                    }
                }
            }
            return RedirectToAction("Index", "Establishments");
        }

        // GET: Institute/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _instituteService.GetByIdAsync(id.Value);
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
            var institute = await _instituteService.GetByIdAsync(id);
            if (institute != null)
            {
                await _instituteService.DeleteAsync(id);
            }

            return RedirectToAction("Index", "Establishments");
        }

        private async Task<bool> InstituteExists(Guid id)
        {
            return await _instituteService.GetByIdAsync(id) != null;
        }
    }
}
