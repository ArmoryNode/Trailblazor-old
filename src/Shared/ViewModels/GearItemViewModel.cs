using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trailblazor.Shared.Models;

namespace Trailblazor.Shared.ViewModels
{
    public record GearItemViewModel(string Id, Guid OwnerId, string OwnerName) : IMapToBaseDocument
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;

        public bool Favorite { get; set; } = false;
        public bool Consumable { get; set; } = false;
        public bool Wearable { get; set; } = false;

        public Weight Weight { get; init; }

        public DateTimeOffset Created { get; init; } = DateTimeOffset.UtcNow;
        public DateTimeOffset LastModified { get; set; } = DateTimeOffset.UtcNow;
    }
}
