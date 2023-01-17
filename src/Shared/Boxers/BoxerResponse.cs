using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtuaBoxer.Shared.Boxers;

public static class BoxerResponse
{
    public class GetIndex
    {
        public IList<BoxerDto.Index> Boxers { get; set; }
        public int TotalAmount { get; set; }
    }

}
