using AnimesProtech.Domain.Entities;

namespace AnimesProtech.Domain.Specification
{
    public class AnimeNameSpecification
    {
        public bool IsSatisfiedBy(String name)
        {
            return !string.IsNullOrEmpty(name);
        }
    }
}
