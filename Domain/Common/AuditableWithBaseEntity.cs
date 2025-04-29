namespace Domain.Common
{
    public abstract class AuditableWithBaseEntity<T> : BaseEntity<T>, IAuditableEntity
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedById { get; set; }
        public long? DeletedById { get ; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
