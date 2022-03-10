using Microsoft.EntityFrameworkCore;

namespace Trailblazor.Shared.Models
{
    [Owned]
    public record Category
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Title { get; init; } = string.Empty;
        public float SortOrder { get; init; } = 0.0f;

        public IEnumerable<GearItem> GearItems { get; set; } = new List<GearItem>();
    }
}
