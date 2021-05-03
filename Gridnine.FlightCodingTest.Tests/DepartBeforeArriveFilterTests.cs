using System;
using Xunit;
using Gridnine.FlightCodingTest.Filter;
using System.Collections.Generic;
using System.Linq;

namespace Gridnine.FlightCodingTest.Tests
{
    public class DepartBeforeArriveFilterTests
    {
        [Fact]
        public void IncludeValidTest()
        {
            IFilter filter = new DepartBeforeArriveFilter();
            var depart = DepartBefore();
            var arrive = ArriveBefore();
            var test = new List<Flight>(depart);
            test.AddRange(arrive);


            var result = filter.Include(new HashSet<Flight>(), test);


            Assert.Equal(arrive, result);
        }

        [Fact]
        public void ExcludeValidTest()
        {
            IFilter filter = new DepartBeforeArriveFilter();
            var depart = DepartBefore();
            var arrive = ArriveBefore();
            var test = new List<Flight>(depart);
            test.AddRange(arrive);


            var result = filter.Exclude(test);


            Assert.Equal(depart, result);
        }

        [Fact]
        public void EmptyIncludeTest()
        {
            IFilter filter = new DepartBeforeArriveFilter();
            var test = DepartBefore();

            var result = filter.Include(new HashSet<Flight>(), test);

            Assert.Empty(result);
        }

        [Fact]
        public void EmptyExcludeTest()
        {
            IFilter filter = new DepartBeforeArriveFilter();
            var test = ArriveBefore();

            var result = filter.Exclude(test);

            Assert.Empty(result);
        }

        public IEnumerable<Flight> DepartBefore()
        {
            var list = new HashSet<Flight>();
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(3), DateTime.Now.AddDays(5)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(-3), DateTime.Now.AddDays(3)));
            return list;
        }

        public IEnumerable<Flight> ArriveBefore()
        {
            var list = new HashSet<Flight>();

            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(3), DateTime.Now.AddDays(-3)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(4), DateTime.Now.AddDays(-4)));
            return list;
        }
    }
}
