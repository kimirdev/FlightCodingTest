using System;
using System.Collections.Generic;
using System.Text;

namespace Gridnine.FlightCodingTest.Filter
{
    public interface IFilter
    {
        public IEnumerable<Flight> Include(IEnumerable<Flight> list, IEnumerable<Flight> source);

        public IEnumerable<Flight> Exclude(IEnumerable<Flight> list);
    }
}
