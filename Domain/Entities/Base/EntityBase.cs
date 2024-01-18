namespace Domain.Entities.Base;

public abstract class EntityBase : IAuditableEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime LastlyEditedAtUtc { get; set; }
    public DateTime? DeletedAtUtc { get; set; } = null;
}