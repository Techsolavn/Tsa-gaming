using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Dto
{
    public class PagingableQuery
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
    }
}
