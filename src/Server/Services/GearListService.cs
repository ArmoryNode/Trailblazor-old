using Microsoft.EntityFrameworkCore;
using Trailblazor.Server.Data;
using Trailblazor.Server.Models.Data;
using Trailblazor.Shared.Models;
using Trailblazor.Shared.Services;
using Trailblazor.Shared.ViewModels;

namespace Trailblazor.Server.Services
{
    public class GearListService : IDataService<GearListViewModel>
    {
        private readonly TrailblazorDbContext _dataContext;
        private readonly ILogger<GearListService> _logger;

        public GearListService(TrailblazorDbContext dataContext, ILogger<GearListService> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<IEnumerable<GearListViewModel>> GetAll(DataServiceQueryOptions queryOptions, CancellationToken cancellationToken)
        {
            using var _ = _logger.BeginScope($"Querying records {queryOptions}");

            try
            {
                var query = _dataContext.GearLists
                    .AsNoTracking()
                    .Select(g => new GearListViewModel
                    {
                        Id = g.Id,
                        OwnerId = g.OwnerId,
                        OwnerName = g.OwnerName,
                        Name = g.Name,
                        Description = g.Description,
                        Categories = g.Categories,
                        LastModified = g.LastModified,
                        Created = g.Created,
                        Deleted = g.Deleted
                    });

                if (queryOptions.OwnerId is Guid ownerId)
                {
                    query = query.Where(g => g.OwnerId == ownerId);
                }

                if (queryOptions.PageNumber is int pageNumber && queryOptions.PageSize is int pageSize)
                {
                    query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                }

                return await query.ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An unexpected error has occurred");
                throw;
            }
        }

        public IAsyncEnumerable<GearListViewModel> GetAllAsync(DataServiceQueryOptions queryOptions)
        {
            using var _ = _logger.BeginScope($"Querying records {queryOptions}");

            try
            {
                var query = _dataContext.GearLists
                    .Select(g => g.ToViewModel());

                var foo = query.ToList();

                if (queryOptions.OwnerId is Guid ownerId)
                {
                    query = query.Where(g => g.OwnerId == ownerId);
                }

                if (queryOptions.PageNumber is int pageNumber && queryOptions.PageSize is int pageSize)
                {
                    query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                }

                return query.AsAsyncEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An unexpected error has occurred");
                throw;
            }
        }

        public async Task<GearListViewModel?> GetById(Guid gearListId, CancellationToken cancellationToken)
        {
            using var _ = _logger.BeginScope($"Getting record by Id {gearListId}");

            try
            {
                return await _dataContext.GearLists
                    .AsNoTracking()
                    .Select(g => new GearListViewModel
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Description = g.Description,
                        Categories = g.Categories.ToList(),
                        OwnerId = g.OwnerId,
                        OwnerName = g.OwnerName,
                        Created = g.Created,
                        LastModified = g.LastModified,
                    })
                    .SingleOrDefaultAsync(g => g.Id == gearListId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An unexpected error has occurred");
                throw;
            }
        }

        public async Task<bool> Insert(GearListViewModel viewModel, CancellationToken cancellationToken)
        {
            using var _ = _logger.BeginScope($"Inserting record {viewModel}");

            try
            {
                var gearItem = GearList.FromViewModel(viewModel);

                var result = _dataContext.GearLists.Add(gearItem);

                await _dataContext.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case DbUpdateConcurrencyException:
                    case DbUpdateException:
                        _logger.LogError(ex, "Failed to insert record");
                        return false;
                    case OperationCanceledException:
                        _logger.LogError(ex, "Insert operation was cancelled");
                        return false;
                    default: // Unhandled exception
                        _logger.LogCritical(ex, "An unexpected error has occurred");
                        throw;
                }
            }
        }

        public async Task<bool> Update(GearListViewModel viewModel, CancellationToken cancellationToken)
        {
            using var _ = _logger.BeginScope($"Updating record {viewModel}");

            try
            {
                var gearItem = GearList.FromViewModel(viewModel);

                var result = _dataContext.GearLists.Update(gearItem);

                await _dataContext.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case DbUpdateConcurrencyException:
                    case DbUpdateException:
                        _logger.LogError(ex, "Failed to update record");
                        return false;
                    case OperationCanceledException:
                        _logger.LogError(ex, "Update operation was cancelled");
                        return false;
                    default: // Unhandled exception
                        _logger.LogCritical(ex, "An unexpected error has occurred");
                        throw;
                }
            }
        }

        public async Task<bool> Remove(GearListViewModel viewModel, CancellationToken cancellationToken)
        {
            using var _ = _logger.BeginScope($"Updating record {viewModel}");

            try
            {
                var gearList = await _dataContext.GearLists.SingleAsync(g => g.Id == viewModel.Id, cancellationToken);

                var result = _dataContext.GearLists.Remove(gearList);

                await _dataContext.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case DbUpdateConcurrencyException:
                    case DbUpdateException:
                        _logger.LogError(ex, "Failed to update record");
                        return false;
                    case OperationCanceledException:
                        _logger.LogError(ex, "Update operation was cancelled");
                        return false;
                    default: // Unhandled exception
                        _logger.LogCritical(ex, "An unexpected error has occurred");
                        throw;
                }
            }
        }
    }
}