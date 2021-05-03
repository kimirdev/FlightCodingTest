using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gridnine.FlightCodingTest.Filter
{
    public class DepartBeforeArriveFilter : IFilter
    {
        public IEnumerable<Flight> Include(IEnumerable<Flight> list, IEnumerable<Flight> source)
        {
            var set = list.ToHashSet();
            source.ToList().ForEach(s =>
            {
                if (s.Segments.Any(x => x.DepartureDate > x.ArrivalDate))
                    set.Add(s);
            });
            return set;
        }

        public IEnumerable<Flight> Exclude(IEnumerable<Flight> list)
        {
            var ret = list.ToList();
            ret.RemoveAll(l => l.Segments.Any(x => x.DepartureDate > x.ArrivalDate));
            return ret;
        }
    }
}
