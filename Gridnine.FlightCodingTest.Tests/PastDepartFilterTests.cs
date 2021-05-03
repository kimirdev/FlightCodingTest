using Gridnine.FlightCodingTest.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Gridnine.FlightCodingTest.Tests
{
    public class PastDepartFilterTests
    {
        [Fact]
        public void IncludeValidTest()
        {
            PastDepartFilter filter = new PastDepartFilter();
            var past = PastDepartList();
            var future = FutureDepartList();
            var test = new List<Flight>(past);
            test.AddRange(future);


            var result = filter.Include(new HashSet<Flight>(), test);


            Assert.Equal(result, past);
        }

        [Fact]
        public void ExcludeValidTest()
        {
            PastDepartFilter filter = new PastDepartFilter();
            var past = PastDepartList();
            var future = FutureDepartList();
            var test = new List<Flight>(past);
            test.AddRange(future);


            var result = filter.Exclude(test);


            Assert.Equal(future, result);
        }

        [Fact]
        public void EmptyIncludeTest()
        {
            PastDepartFilter filter = new PastDepartFilter();
            var test = FutureDepartList();

            var result = filter.Include(new HashSet<Flight>(), test);

            Assert.Empty(result);
        }


        [Fact]
        public void EmptyExcludeTest()
        {
            PastDepartFilter filter = new PastDepartFilter();
            var test = PastDepartList();

            var result = filter.Exclude(test);

            Assert.Empty(result);
        }

        public HashSet<Flight> PastDepartList()
        {
            var list = new HashSet<Flight>();
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-2)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-2)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-3)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(-6), DateTime.Now.AddDays(1)));
            return list;
        }

        public HashSet<Flight> FutureDepartList()
        {
            var list = new HashSet<Flight>();

            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(3), DateTime.Now.AddDays(5)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(4), DateTime.Now.AddDays(5)));
            list.Add(FlightBuilder.CreateFlight(DateTime.Now.AddDays(5), DateTime.Now.AddDays(7)));
            return list;
        }
    }
}
