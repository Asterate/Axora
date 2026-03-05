using App.DAL.EF;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class MaterialPageController : Controller
{
    private readonly AppDbContext _context;

    public MaterialPageController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult CreateMaterial(Guid courseId)
    {
        var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
        if (course == null)
        {
            return NotFound();
        }

        var vm = new CreateMaterialViewModel
        {
            CourseId = courseId,
            CourseName = course.CourseName
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMaterial(Guid courseId, CreateMaterialViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var material = new Material
            {
                CourseId = courseId,
                MaterialName = vm.MaterialName,
                MaterialDescription = vm.MaterialDescription,
                MaterialVersion = vm.MaterialVersion,
                MaterialEnvironment = vm.MaterialEnvironment
            };

            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return RedirectToAction("CourseDesktop", "CoursePage", new { id = courseId });
        }

        // If model state is invalid, reload course name for the view
        var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
        vm.CourseName = course?.CourseName;
        return View(vm);
    }

    public IActionResult EditMaterial(Guid materialId)
    {
        var material = _context.Materials.FirstOrDefault(m => m.Id == materialId);
        if (material == null)
        {
            return NotFound();
        }

        var course = _context.Courses.FirstOrDefault(c => c.Id == material.CourseId);
        if (course == null)
        {
            return NotFound();
        }

        var vm = new CreateMaterialViewModel
        {
            CourseId = material.CourseId,
            CourseName = course.CourseName,
            MaterialName = material.MaterialName,
            MaterialDescription = material.MaterialDescription,
            MaterialVersion = material.MaterialVersion,
            MaterialEnvironment = material.MaterialEnvironment
        };

        ViewData["MaterialId"] = materialId;
        ViewData["IsEditMode"] = true;
        ViewData["Title"] = "Edit Material";

        return View("CreateMaterial", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditMaterial(Guid materialId, CreateMaterialViewModel vm)
    {
        var material = _context.Materials.FirstOrDefault(m => m.Id == materialId);
        if (material == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            material.MaterialName = vm.MaterialName;
            material.MaterialDescription = vm.MaterialDescription;
            material.MaterialVersion = vm.MaterialVersion;
            material.MaterialEnvironment = vm.MaterialEnvironment;

            _context.Materials.Update(material);
            await _context.SaveChangesAsync();

            return RedirectToAction("CourseDesktop", "CoursePage", new { id = material.CourseId });
        }

        // If model state is invalid, reload course name for the view
        var course = _context.Courses.FirstOrDefault(c => c.Id == vm.CourseId);
        vm.CourseName = course?.CourseName;
        ViewData["MaterialId"] = materialId;
        ViewData["IsEditMode"] = true;
        ViewData["Title"] = "Edit Material";

        return View("CreateMaterial", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteMaterial(Guid materialId)
    {
        var material = _context.Materials.FirstOrDefault(m => m.Id == materialId);
        if (material != null)
        {
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("CourseDesktop", "CoursePage", new { id = material?.CourseId });
    }
}
