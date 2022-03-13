using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Trailblazor.Shared.Models;
using Trailblazor.Shared.ViewModels;

#nullable enable

namespace Trailblazor.Shared.Services
{
    public interface IDataService<TViewModel>
    {
        Task<IEnumerable<TViewModel>> GetAll(DataServiceQueryOptions queryOptions, CancellationToken cancellationToken);
        Task<IEnumerable<TViewModel>> GetAllPartial(DataServiceQueryOptions options, CancellationToken cancellationToken);
        Task<TViewModel?> GetById(Guid objectId, CancellationToken cancellationToken);
        Task<bool> Insert(TViewModel viewModel, CancellationToken cancellationToken);
        Task<bool> Update(TViewModel viewModel, CancellationToken cancellationToken);
        Task<bool> Remove(TViewModel viewModel, CancellationToken cancellationToken);
    }
}
