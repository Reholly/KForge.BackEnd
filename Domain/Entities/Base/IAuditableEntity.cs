namespace Domain.Entities.Base;

public interface IAuditableEntity
{
    public DateTime CreatedAtUtc { get; set; }
    public DateTime LastlyEditedAtUtc { get; set; }
    public DateTime? DeletedAtUtc { get; set; }
}