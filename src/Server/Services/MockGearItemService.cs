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

            InitializeGearItems();
        }

        public Task<IEnumerable<GearItemViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var viewModels = _gearItems.Select(i => new GearItemViewModel(i.Id, i.OwnerId, i.OwnerName)
            {
                Name = i.Name,
                Description = i.Description,

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

                    Created = entity.Created,
                    LastModified = entity.LastModified,

                    Consumable = entity.Consumable,
                    Favorite = entity.Favorite,
                    Wearable = entity.Wearable
                };
            }

            return Task.FromResult(viewModel);
        }

        private void InitializeGearItems()
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
                    Link = "https://google.com?q=backpack",
                    LastModified = DateTime.UtcNow,
                    Created = DateTimeOffset.UtcNow,
                },
                new GearItem("61ca5f74bf74701671b07c46", userId, "You!")
                {
                    Name = "Trekking Poles",
                    Description = "Poles for trekking",
                    Link = "https://google.com?q=trekking_poles",
                    LastModified = DateTime.UtcNow,
                    Created = DateTimeOffset.UtcNow,
                },
                new GearItem("61ca5f74bf74701671b07c46", userId, "Someone else.")
                {
                    Name = "Someone Else's",
                    Description = "You shouldn't be able to see this.",
                    Link = "https://httpstatuses.com/403",
                    LastModified = DateTime.UtcNow,
                    Created = DateTimeOffset.UtcNow,
                }
            };

            _gearItems.AddRange(gearItems);
        }
    }
}