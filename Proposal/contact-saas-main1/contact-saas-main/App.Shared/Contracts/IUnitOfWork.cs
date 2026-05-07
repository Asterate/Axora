namespace App.Shared.Contracts;

public interface IUnitOfWork
{ 
    Task<int> SaveChangesAsync();
}