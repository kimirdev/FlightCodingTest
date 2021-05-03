using Gridnine.FlightCodingTest.Filter;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Gridnine.FlightCodingTest.Tests
{
    public class LongGroundTimeFilterTests
    {
        [Fact]
        public void IncludeValidTest()
        {
            IFilter filter = new LongGroundTimeFilter();
            var longTime = LongTimeList();
            var shortTime = ShortTimeList();
            var test = new List<Flight>(longTime);
            test.AddRange(shortTime);


            var result = filter.Include(new HashSet<Flight>(), test);


            Assert.Equal(result, longTime);
        }

        [Fact]
        public void ExcludeValidTest()
        {
            IFilter filter = new LongGroundTimeFilter();
            var longTime = LongTimeList();
            var shortTime = ShortTimeList();
            var test = new List<Flight>(longTime);
            test.AddRange(shortTime);


            var result = filter.Exclude(test);


            Assert.Equal(shortTime, result);
        }

        [Fact]
        public void EmptyIncludeTest()
        {
            IFilter filter = new LongGroundTimeFilter();
            var test = ShortTimeList();

            var result = filter.Include(new HashSet<Flight>(), test);

            Assert.Empty(result);
        }


        [Fact]
        public void EmptyExcludeTest()
        {
            IFilter filter = new LongGroundTimeFilter();
            var test = LongTimeList();

            var result = filter.Exclude(test);

            Assert.Empty(result);
        }

        public HashSet<Flight> LongTimeList()
        {
            var list = new HashSet<Flight>();

            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), DateTime.Now.AddDays(3)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-2).AddHours(3), DateTime.Now.AddDays(6)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-3), DateTime.Now, DateTime.Now.AddDays(2)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(-6), DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(2).AddMinutes(30), DateTime.Now.AddDays(2)));
            return list;
        }

        public HashSet<Flight> ShortTimeList()
        {
            var list = new HashSet<Flight>();

            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(3), DateTime.Now.AddDays(5), DateTime.Now.AddDays(5).AddHours(1), DateTime.Now.AddDays(10)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1).AddMinutes(59), DateTime.Now.AddDays(-2)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(4), DateTime.Now.AddDays(5)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(5), DateTime.Now.AddDays(7)));
            return list;
        }
    }
}
