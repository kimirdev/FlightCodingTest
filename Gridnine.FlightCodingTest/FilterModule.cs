using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gridnine.FlightCodingTest.Filter;
using Gridnine.FlightCodingTest.InOut;

namespace Gridnine.FlightCodingTest
{
    class FilterModule
    {
        private IEnumerable<Flight> Source;
        public IEnumerable<Flight> Flights;

        public FilterModule(IEnumerable<Flight> flights)
        {
            Source = flights;
            Flights = new HashSet<Flight>(Source);
        }

        public void Filter()
        {
            IFilter filter;

            Writer.Filter();
            filter = Reader.Filter();

            Writer.IncludeOrExclude();
            var temp = Reader.IncludeOrExclude(filter, Flights, Source);
            if (temp != null)
                Flights = temp;
        }

        public void EmptyList()
        {
            Flights = new HashSet<Flight>();
        }

        public void AddFlight()
        {
            var src = Source.ToList();
            int segCount = Reader.NumberOfSegments();
            DateTime[] dateTimes;
            try
            {
                dateTimes = Reader.DepartArriveDates(segCount);
            }
            catch (Exception)
            {
                return;
            }
            var flight = FlightBuilder.CreateFlight(dateTimes);

            src.Add(flight);
            Source = src;
            src = Flights.ToList();
            src.Add(flight);
            Flights = src;
        }

        public void Fill()
        {
            Flights = new HashSet<Flight>(Source);
        }
    }
}
