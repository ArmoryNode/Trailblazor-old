using Trailblazor.Shared.Models;
using Trailblazor.Shared.ViewModels;

namespace Trailblazor.Server.Models.Data
{
    public record GearItem(string Id, Guid OwnerId, string OwnerName) : BaseDocument(Id)
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;

        public bool Favorite { get; set; } = false;
        public bool Consumable { get; set; } = false;
        public bool Wearable { get; set; } = false;

        public Weight Weight { get; set; } = new();

        public GearItem UpdateFrom(GearItemViewModel viewModel)
        {
            return this with
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Link = viewModel.Link,

                Favorite = viewModel.Favorite,
                Consumable = viewModel.Consumable,
                Wearable = viewModel.Wearable,

                Weight = viewModel.Weight,

                LastModified = DateTimeOffset.UtcNow
            };
        }
    }
}
