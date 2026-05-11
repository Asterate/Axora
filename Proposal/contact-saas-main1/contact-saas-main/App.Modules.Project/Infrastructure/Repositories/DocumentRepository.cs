using App.Domain.Entities;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Infrastructure;
using Microsoft.EntityFrameworkCore;

internal class DocumentRepository : IDocumentRepository
{
    private readonly ProjectDbContext _context;

    public DocumentRepository(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Document>> GetAllAsync()
        => await _context.Documents.Include(d => d.DocumentType).ToListAsync();

    public async Task<Document?> GetByIdAsync(Guid id)
        => await _context.Documents
            .Include(d => d.DocumentType)
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task AddAsync(Document entity)
        => await _context.Documents.AddAsync(entity);

    public void Update(Document entity)
        => _context.Documents.Update(entity);

    public void Delete(Document entity)
        => _context.Documents.Remove(entity);
}