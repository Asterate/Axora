namespace App.Shared.Domain;

public abstract class BaseEntity: IBaseEntity
{
    public Guid Id { get; set; } =  Guid.NewGuid();
}