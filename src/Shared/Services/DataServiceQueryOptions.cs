using System;
using System.Linq;

#nullable enable

namespace Trailblazor.Shared.Services
{
    public record struct DataServiceQueryOptions
    {
        public Guid? OwnerId { get; init; }
        public int? PageNumber { get; init; }
        public int? PageSize { get; init; }
        public bool IncludeDeleted { get; init; }
        public bool Simple { get; init; }
    }
}
