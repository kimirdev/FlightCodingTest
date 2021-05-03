using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gridnine.FlightCodingTest.Filter
{
    public class PastDepartFilter : IFilter
    {
        private DateTime date = DateTime.Now;

        public IEnumerable<Flight> Include(IEnumerable<Flight> list, IEnumerable<Flight> source)
        {
            var set = list.ToHashSet();
            source.ToList().ForEach(s =>
            {
                if (s.Segments.FirstOrDefault(s => s.DepartureDate < date) != null)
                    set.Add(s);
            });
            return set;
        }

        public IEnumerable<Flight> Exclude(IEnumerable<Flight> list)
        {
            var ret = list.ToList();
            ret.RemoveAll(l => l.Segments.FirstOrDefault(s => s.DepartureDate < date) != null);
            return ret;
        }
    }
}
