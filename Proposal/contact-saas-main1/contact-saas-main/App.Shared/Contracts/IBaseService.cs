namespace App.Shared.Contracts;

public interface IBaseService<T>
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task CreateAsync(T dto);
    Task UpdateAsync(T dto);
    Task DeleteAsync(Guid id);
}