using AnimesProtech.Domain.Specification;

namespace AnimesProtech.Domain.Entities
{
    public class Anime : BaseEntity
    {
        private readonly AnimeNameSpecification _animeNameSpecification;

        public string name { get; private set; }
        public string? summary { get; set; }
        public string? director { get; set; }

        public Anime()
        {
            _animeNameSpecification = new AnimeNameSpecification();
        }

        public void ChangeName(string newName)
        {
            if (!_animeNameSpecification.IsSatisfiedBy(newName))
            {
                throw new ArgumentException("O nome não pode ser vazio ou nulo.", nameof(newName));
            }

            name = newName;
        }

    }
}
