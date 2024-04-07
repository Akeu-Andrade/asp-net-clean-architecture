namespace AnimesProtech.Domain.Entities
{
    public class BaseEntity
    {
        public Guid id { get; set; }
        public DateTimeOffset created_at { get; set; }
        public DateTimeOffset? updated_at { get; set; }
        public DateTimeOffset? deleted_at { get; set; }
    }
}
