namespace Domain.Common
{
    public interface IAuditableEntity
    {
        bool IsDeleted { get; set; }
        DateTime CreatedOn { get; set; }
        long CreatedById { get; set; }
        DateTime? ModifiedOn { get; set; }
        long? ModifiedById { get; set; }
        public long? DeletedById { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
