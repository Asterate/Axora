using App.Domain.Entities;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Domain;
using App.Modules.Project.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure.Repositories;

internal sealed class DocumentTypeRepository : IDocumentTypeRepository
{
    private readonly ResearchDbContext _context;

    public DocumentTypeRepository(ResearchDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DocumentType>> GetAllAsync()
        => await _context.DocumentTypes.ToListAsync();

    public async Task<DocumentType?> GetByIdAsync(Guid id)
        => await _context.DocumentTypes.FindAsync(id);

    public async Task AddAsync(DocumentType entity)
        => await _context.DocumentTypes.AddAsync(entity);

    public void Update(DocumentType entity)
        => _context.DocumentTypes.Update(entity);

    public void Delete(DocumentType entity)
        => _context.DocumentTypes.Remove(entity);
}