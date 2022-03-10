using Microsoft.EntityFrameworkCore;
using Trailblazor.Shared.Models;

namespace Trailblazor.Shared.Models
{
    [Owned]
    public class GearItem
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Link { get; set; } = string.Empty;

        public bool Consumable { get; set; } = false;
        public bool Wearable { get; set; } = false;
        public bool Favorite { get; set; } = false;

        public int Quantity { get; set; } = 1;
        public float SortOrder { get; set; } = 0.0f;

        public Weight Weight { get; init; } = new();
    }
}
