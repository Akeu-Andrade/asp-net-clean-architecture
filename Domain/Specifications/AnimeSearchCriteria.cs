namespace AnimesProtech.Domain.Specifications
{
    public class AnimeSearchCriteria
    {
        public string? Director { get; set; }
        public string? Name { get; set; }
        public string? Keywords { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
