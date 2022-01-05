using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Trailblazor.Shared.Models;

#nullable enable

namespace Trailblazor.Shared.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
        Task<T?> GetById(string objectId, CancellationToken cancellationToken);
    }
}
