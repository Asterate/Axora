
using App.Domain.Entities;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Domain;
using App.Modules.Project.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal sealed class DocumentResultRepository : IDocumentResultRepository
{
    private readonly ResearchDbContext _context;

    public DocumentResultRepository(ResearchDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DocumentResult>> GetAllAsync()
        => await _context.DocumentResults.ToListAsync();

    public async Task<DocumentResult?> GetByIdAsync(Guid id)
        => await _context.DocumentResults.FindAsync(id);

    public async Task AddAsync(DocumentResult entity)
        => await _context.DocumentResults.AddAsync(entity);

    public void Update(DocumentResult entity)
        => _context.DocumentResults.Update(entity);

    public void Delete(DocumentResult entity)
        => _context.DocumentResults.Remove(entity);
}