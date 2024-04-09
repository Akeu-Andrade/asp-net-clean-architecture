using AnimesProtech.Domain.Specification;

namespace AnimesProtech.Domain.Entities
{
    public class Anime : BaseEntity
    {
        private readonly AnimeNameSpecification _animeNameSpecification;

        public string name { get; set; }
        public string? summary { get; set; }
        public string? director { get; set; }

    }
}
