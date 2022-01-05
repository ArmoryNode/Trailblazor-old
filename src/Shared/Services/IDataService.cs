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
        Task<IEnumerable<TViewModel>> GetAll(CancellationToken cancellationToken);
        Task<TViewModel?> GetById(string objectId, CancellationToken cancellationToken);
        Task<bool> Upsert(TViewModel viewModel, CancellationToken cancellationToken);
        Task<bool> Remove(TViewModel viewModel, CancellationToken cancellationToken);
    }
}
