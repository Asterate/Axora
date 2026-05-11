using App.Domain.Entities;
using App.Modules.Experiment.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Experiment.Infrastructure.Repositories;

internal sealed class ExperimentEquipmentRepository : IExperimentEquipmentRepository
{
    private readonly ExperimentDbContext _context;

    public ExperimentEquipmentRepository(ExperimentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExperimentEquipment>> GetAllAsync()
        => await _context.ExperimentEquipments.ToListAsync();

    public async Task<ExperimentEquipment?> GetByIdAsync(Guid id)
        => await _context.ExperimentEquipments.FindAsync(id);

    public async Task AddAsync(ExperimentEquipment entity)
        => await _context.ExperimentEquipments.AddAsync(entity);

    public void Update(ExperimentEquipment entity)
        => _context.ExperimentEquipments.Update(entity);

    public void Delete(ExperimentEquipment entity)
        => _context.ExperimentEquipments.Remove(entity);
}