using System.Text.Json.Serialization;
using Trailblazor.Shared.Models;
using Trailblazor.Shared.ViewModels;

namespace Trailblazor.Server.Models.Data
{
    public record GearList(Guid Id, Guid OwnerId, string OwnerName) : DocumentBase(Id), ISoftDeletable
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;

        public ICollection<Category> Categories { get; init; } = new List<Category>();

        public DateTimeOffset? Deleted { get; set; } = default;

        public GearList UpdateFrom(GearListViewModel viewModel)
        {
            return this with
            {
                Id = viewModel.Id,  
                Name = viewModel.Name,
                Description = viewModel.Description,
                Categories = viewModel.Categories.ToList(),
                LastModified = viewModel.LastModified,
            };
        }

        public static GearList FromViewModel(GearListViewModel viewModel)
        {
            return new GearList(viewModel.Id, viewModel.OwnerId, viewModel.OwnerName)
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Categories = viewModel.Categories.ToList(),
                LastModified = viewModel.LastModified,
                Created = viewModel.Created,
                Deleted = viewModel.Deleted
            };
        }

        // NOTE: Be mindful when using this in EF Core queries. This bypasses projections.
        public GearListViewModel ToViewModel()
        {   
            return new GearListViewModel
            {
                Id = Id,
                OwnerId = OwnerId,
                OwnerName = OwnerName,
                Name = Name,
                Description = Description,
                Categories = Categories.ToList(),
                LastModified = LastModified,
                Created = Created,
                Deleted = Deleted
            };
        }
    }
}
