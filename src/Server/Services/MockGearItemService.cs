using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Principal;
using Trailblazor.Server.Infrastructure;
using Trailblazor.Server.Models.Data;
using Trailblazor.Shared;
using Trailblazor.Shared.Models;
using Trailblazor.Shared.Services;
using Trailblazor.Shared.ViewModels;

namespace Trailblazor.Server.Services
{
    public class MockGearItemService : IDataService<GearItemViewModel>
    {
        private readonly List<GearItem> _gearItems = new();
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MockGearItemService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

            SeedMockGearItems();
        }

        public Task<IEnumerable<GearItemViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var viewModels = _gearItems.Select(i => new GearItemViewModel(i.Id, i.OwnerId, i.OwnerName)
            {
                Name = i.Name,
                Description = i.Description,
                Link = i.Link,

                Created = i.Created,
                LastModified = i.LastModified,

                Consumable = i.Consumable,
                Favorite = i.Favorite,
                Wearable = i.Wearable
            });

            return Task.FromResult(viewModels);
        }

        public Task<GearItemViewModel?> GetById(string objectId, CancellationToken cancellationToken)
        {
            var entity = _gearItems.FirstOrDefault(i => i.Id == objectId);

            GearItemViewModel? viewModel;

            if (entity == null)
            {
                viewModel = null;
            }
            else
            {
                viewModel = new GearItemViewModel(entity.Id, entity.OwnerId, entity.OwnerName)
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    Link = entity.Link,

                    Created = entity.Created,
                    LastModified = entity.LastModified,

                    Consumable = entity.Consumable,
                    Favorite = entity.Favorite,
                    Wearable = entity.Wearable
                };
            }

            return Task.FromResult(viewModel);
        }

        public Task<bool> Remove(GearItemViewModel viewModel, CancellationToken cancellationToken)
        {
            var gearItem = _gearItems.SingleOrDefault(i => i.Id == viewModel.Id);
            if (gearItem == null)
                return Task.FromResult(false);

            var removed = _gearItems.Remove(gearItem);

            return Task.FromResult(removed);
        }

        public Task<bool> Upsert(GearItemViewModel viewModel, CancellationToken cancellationToken)
        {
            var gearItem = _gearItems.SingleOrDefault(i => i.Id == viewModel.Id);
            if (gearItem != null)
                _gearItems.Remove(gearItem);

            var newGearItem = new GearItem(viewModel.Id, viewModel.OwnerId, viewModel.OwnerName);

            newGearItem.UpdateFrom(viewModel);

            _gearItems.Add(newGearItem);

            return Task.FromResult(true);
        }

        private void SeedMockGearItems()
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

            var hasUserId = httpContextAccessor.HttpContext!.User.TryGetUserId<Guid>(out var userId);

            if (!hasUserId)
                userId = Guid.Empty;

            List<GearItem> gearItems = new()
            {
                new GearItem("61c93cae14686471590850ee", userId, "You!")
                {
                    Name = "Backpack",
                    Description = "The coolest backpack I own",
                    Link = "https://images.google.com/search?q=backpack",
                    LastModified = DateTime.UtcNow,
                    Created = DateTimeOffset.UtcNow,
                    Weight = new Weight(3, WeightUnit.Pounds),
                },
                new GearItem("61ca5f74bf74701671b07c46", userId, "You!")
                {
                    Name = "Trekking Poles",
                    Description = "Poles for trekking",
                    Link = "https://images.google.com/search?q=trekking%20poles",
                    LastModified = DateTime.UtcNow,
                    Created = DateTimeOffset.UtcNow,
                    Weight = new Weight(3, WeightUnit.Pounds),
                },
                new GearItem("61f22833278f55c1a7c72fc3", Guid.NewGuid(), "You!")
                {
                    Name = "Hat",
                    Description = "My favorite hat",
                    Link = "https://images.google.com/search?q=hat",
                    LastModified = DateTime.UtcNow,
                    Created = DateTimeOffset.UtcNow,
                    Weight = new Weight(16, WeightUnit.Ounces),
                },
                new GearItem("61ca5f74bf74701671b07c46", Guid.NewGuid(), "Someone else.")
                {
                    Name = "Someone Else's",
                    Description = "You shouldn't be able to see this.",
                    Link = "https://httpstatuses.com/403",
                    LastModified = DateTime.UtcNow,
                    Created = DateTimeOffset.UtcNow,
                    Weight = new Weight(-1),
                },
            };

            _gearItems.AddRange(gearItems);
        }
    }
}