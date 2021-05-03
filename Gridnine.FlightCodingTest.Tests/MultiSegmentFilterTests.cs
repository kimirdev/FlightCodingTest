using System;
using Xunit;
using Gridnine.FlightCodingTest.Filter;
using System.Collections.Generic;
using System.Linq;

namespace Gridnine.FlightCodingTest.Tests
{
    public class MultiSegmentFilterTests
    {
        [Fact]
        public void IncludeValidTest()
        {
            MultiSegmentFilter filter = new MultiSegmentFilter();
            var test = SingleSegmentList().ToList();
            var multi = MultiSegmentList();
            test.AddRange(multi);


            var result = filter.Include(new HashSet<Flight>(), test);


            Assert.Equal(result, multi);
        }

        [Fact]
        public void ExcludeValidTest()
        {
            MultiSegmentFilter filter = new MultiSegmentFilter();
            var single = SingleSegmentList();
            var multi = MultiSegmentList();
            var test = new List<Flight>(single);
            test.AddRange(multi);


            var result = filter.Exclude(test);


            Assert.Equal(result, single);
        }

        [Fact]
        public void EmptyIncludeTest()
        {
            MultiSegmentFilter filter = new MultiSegmentFilter();
            var test = SingleSegmentList();

            var result = filter.Include(new HashSet<Flight>(), test);

            Assert.Empty(result);
        }


        [Fact]
        public void EmptyExcludeTest()
        {
            MultiSegmentFilter filter = new MultiSegmentFilter();
            var test = MultiSegmentList();

            var result = filter.Exclude(test);

            Assert.Empty(result);
        }

        public HashSet<Flight> SingleSegmentList()
        {
            var list = new HashSet<Flight>();
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(3), DateTime.Now.AddDays(5)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(-3), DateTime.Now.AddDays(3)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(3), DateTime.Now.AddDays(-3)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(3), DateTime.Now.AddDays(-3)));
            return list;
        }

        public HashSet<Flight> MultiSegmentList()
        {
            var list = new HashSet<Flight>();

            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(3), DateTime.Now.AddDays(5), DateTime.Now.AddDays(3), DateTime.Now.AddDays(-3)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(-3), DateTime.Now.AddDays(3), DateTime.Now.AddDays(3), DateTime.Now.AddDays(-3)));
            return list;
        }
    }
}
