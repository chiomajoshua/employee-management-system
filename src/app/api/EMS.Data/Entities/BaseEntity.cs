namespace EMS.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid CreatedById { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public Guid UpdatedById { get; set; }
    }
}