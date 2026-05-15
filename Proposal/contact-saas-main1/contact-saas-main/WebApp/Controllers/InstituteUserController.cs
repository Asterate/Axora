using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Entities;
using App.Modules.Identity.Application.Services;
using App.Modules.Identity.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class InstituteUserController : Controller
    {
        private readonly InstituteUserService _instituteUserService;

        public InstituteUserController(InstituteUserService instituteUserService)
        {
            _instituteUserService = instituteUserService;
        }

        // GET: InstituteUser
        public async Task<IActionResult> Index()
        {
            var instituteUser = await _instituteUserService.GetAllAsync();
            return View(instituteUser);
        }

        // GET: InstituteUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteUser = await _instituteUserService.GetByIdAsync(id.Value);
            if (instituteUser == null)
            {
                return NotFound();
            }

            return View(instituteUser);
        }

        // GET: InstituteUser/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: InstituteUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection formData)
        {
            var instituteUser = new InstituteUser
            {
                Id = Guid.NewGuid(),
                InstituteId = Guid.Parse(formData["InstituteId"]!),
                UserId = Guid.Parse(formData["UserId"]!),
                Role = Enum.Parse<EInstituteUserRole>(formData["Role"]!)
            };

            if (ModelState.IsValid)
            {
                var newInstituteUser = new CreateInstituteUserRequest()
                {
                    Id =  instituteUser.Id,
                    InstituteId = instituteUser.InstituteId,
                    UserId = instituteUser.UserId,
                };
                await _instituteUserService.CreateAsync(newInstituteUser);
                return RedirectToAction(nameof(Index));
            }
            return View(instituteUser);
        }

        // GET: InstituteUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteUser = await _instituteUserService.GetByIdAsync(id.Value);
            if (instituteUser == null)
            {
                return NotFound();
            }
            return View(instituteUser);
        }

        // POST: InstituteUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InstituteId,Role,Id")] InstituteUser instituteUser)
        {
            if (id != instituteUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var update = new UpdateInstituteUserRequest(instituteUser);
                    await _instituteUserService.UpdateAsync(id, update);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InstituteUserExists(instituteUser.Id))
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
            return View(instituteUser);
        }

        // GET: InstituteUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituteUser = await _instituteUserService.GetByIdAsync(id.Value);
            if (instituteUser == null)
            {
                return NotFound();
            }

            return View(instituteUser);
        }

        // POST: InstituteUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var instituteUser = await  _instituteUserService.GetByIdAsync(id);
            if (instituteUser != null)
            {
                await _instituteUserService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> InstituteUserExists(Guid id)
        {
            return await _instituteUserService.GetByIdAsync(id) != null;
        }
    }
}
