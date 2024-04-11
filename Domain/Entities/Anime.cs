namespace AnimesProtech.Domain.Entities
{
    public class Anime : BaseEntity
    {
        public string name { get; set; }
        public string? summary { get; set; }
        public string? director { get; set; }

    }
}
