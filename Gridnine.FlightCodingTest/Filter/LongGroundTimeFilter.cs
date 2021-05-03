using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Gridnine.FlightCodingTest.Filter
{
    public class LongGroundTimeFilter : IFilter
    {
        public static int LongTime = 2;
        public IEnumerable<Flight> Include(IEnumerable<Flight> list, IEnumerable<Flight> source)
        {
            var set = list.ToHashSet();
            source.ToList().ForEach(f =>
            {
                TimeSpan time = new TimeSpan();
                if (f.Segments.Count > 1)
                {
                    for (int i = f.Segments.Count - 1; i > 0; i--)
                        time = time.Add(f.Segments[i].DepartureDate.Subtract(f.Segments[i - 1].ArrivalDate));
                }
                if (time.TotalHours >= 2)
                    set.Add(f);
            });
            return set;
        }

        public IEnumerable<Flight> Exclude(IEnumerable<Flight> list)
        {
            var ret = list.ToList();
            ret.RemoveAll(f =>
            {
                TimeSpan time = new TimeSpan(0);
                if (f.Segments.Count > 1)
                {
                    for (int i = f.Segments.Count - 1; i > 0; i--)
                        time = time.Add(f.Segments[i].DepartureDate.Subtract(f.Segments[i - 1].ArrivalDate));
                }
                if (time.TotalHours >= 2.0)
                    return true;
                return false;
            });
            return ret;
        }
    }
}
