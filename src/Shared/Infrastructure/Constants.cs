using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trailblazor.Shared.Infrastructure
{
    public struct Authentication
    {
        public struct CustomClaimTypes
        {
            public const string Image = "image";
            public const string FirstName = "given_name";
            public const string LastName = "family_name";
        }
    }
}
