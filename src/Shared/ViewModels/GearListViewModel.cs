using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trailblazor.Shared.Models;

namespace Trailblazor.Shared.ViewModels
{
    public class GearListViewModel
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; init; }
        public string OwnerName { get; init; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
            
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

        public DateTimeOffset Created { get; init; }
        public DateTimeOffset LastModified { get; set; }
        public DateTimeOffset? Deleted { get; set; }
    }
}
