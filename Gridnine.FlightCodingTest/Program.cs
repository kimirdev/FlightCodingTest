using Gridnine.FlightCodingTest.InOut;
using System;
using System.Collections.Generic;

namespace Gridnine.FlightCodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Writer.Greeting();
            FlightBuilder builder = new FlightBuilder();
            FilterModule module = new FilterModule(builder.GetFlights());
            
            bool exit = true;
            do
            {
                exit = Reader.MainMenuResponse(ref module, builder);
            } while (exit);
        }
    }
}
