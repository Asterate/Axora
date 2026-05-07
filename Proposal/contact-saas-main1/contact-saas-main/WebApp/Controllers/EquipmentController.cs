using Microsoft.AspNetCore.Mvc;

namespace App.WebApp.Controllers;

public class EquipmentController : Controller
{
    private readonly EquipmentService _equipmentService;

    public EquipmentController(EquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    // GET: /Equipment
    public async Task<IActionResult> Index()
    {
        var equipment = await _equipmentService.GetAllAsync();
        return View(equipment);
    }

    // GET: /Equipment/Details/id
    public async Task<IActionResult> Details(Guid id)
    {
        var equipment = await _equipmentService.GetByIdAsync(id);
        if (equipment == null) return NotFound();
        return View(equipment);
    }

    // GET: /Equipment/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Equipment/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEquipmentRequest request)
    {
        if (!ModelState.IsValid) return View(request);
        await _equipmentService.CreateAsync(request);
        return RedirectToAction(nameof(Index));
    }

    // GET: /Equipment/Edit/id
    public async Task<IActionResult> Edit(Guid id)
    {
        var equipment = await _equipmentService.GetByIdAsync(id);
        if (equipment == null) return NotFound();
        return View(equipment);
    }

    // POST: /Equipment/Edit/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UpdateEquipmentRequest request)
    {
        if (!ModelState.IsValid) return View(request);
        await _equipmentService.UpdateAsync(id, request);
        return RedirectToAction(nameof(Index));
    }

    // POST: /Equipment/Delete/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _equipmentService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}