using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trailblazor.Shared.Models
{
    public interface IMapToBaseDocument
    {
        string Id { get; init; }

        public DateTimeOffset Created { get; init; }
        public DateTimeOffset LastModified { get; set; }
    }
}
