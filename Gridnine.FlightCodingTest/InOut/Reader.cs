using Gridnine.FlightCodingTest.Filter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gridnine.FlightCodingTest.InOut
{
    class Reader
    {
        internal static IFilter Filter()
        {
            int response;
            IFilter filter;

            response = GetNumber(1, 4);
            switch (response)
            {
                case 1:
                    filter = new DepartBeforeArriveFilter();
                    break;
                case 2:
                    filter = new LongGroundTimeFilter();
                    break;
                case 3:
                    filter = new MultiSegmentFilter();
                    break;
                case 4:
                    filter = new PastDepartFilter();
                    break;
                default:
                    filter = new DepartBeforeArriveFilter();
                    break;
            }
            return filter;
        }

        internal static int NumberOfSegments()
        {
            Writer.NumberOfSegments();
            return GetNumber(1, 10);
        }

        internal static DateTime[] DepartArriveDates(int segCount)
        {
            List<DateTime> ret = new List<DateTime>();
            for (int i = 0; i < segCount; i++)
            {
                Writer.WriteColor("Type '[back]' to go back to main menu", ConsoleColor.Magenta);
                Console.WriteLine($"Type Departure date for segment #{i+1}\n");
                ret.Add(GetDate());
                Console.WriteLine($"Type Arrival date for segment #{i+1}\n");
                ret.Add(GetDate());
            }
            return ret.ToArray();
        }

        internal static bool MainMenuResponse(ref FilterModule module, FlightBuilder builder)
        {
            Writer.MainMenu();

            var response = Console.ReadLine();

            switch (response)
            {
                case "list": 
                    Writer.WriteList(module.Flights);
                    break;
                case "filter":
                    module.Filter();
                    break;
                case "add":
                    module.AddFlight();
                    break;
                case "fill":
                    module.Fill();
                    break;
                case "empty":
                    module.EmptyList();
                    break;
                case "reset":
                    module = new FilterModule(builder.GetFlights());
                    break;
                case "exit":
                    return false;
                default:
                    Writer.WrongInput();
                    break;
            }
            return true;
        }

        internal static IEnumerable<Flight> IncludeOrExclude(IFilter filter,
            IEnumerable<Flight> Flights, IEnumerable<Flight> Source)
        {
            int response = GetNumber(1, 4);

            switch (response)
            {
                case 1: //Include
                    return filter.Include(Flights, Source);
                case 2: //Exclude
                    return filter.Exclude(Flights);
                case 3: //Show with
                    Writer.WriteList(filter.Include(new HashSet<Flight>(), Source));
                    break;
                case 4: //Show without
                    Writer.WriteList(filter.Exclude(Source));
                    break;
            }
            return null;
        }

        private static int GetNumber(int from, int to)
        {
            int response;
            try
            {
                response = Int32.Parse(Console.ReadLine());
                if (response < from || response > to)
                    throw new Exception($"Type number from {from} to {to}");
            }
            catch (Exception ex)
            {
                Writer.WrongInput(ex.Message);
                return GetNumber(from, to);
            }
            return response;
        }
        private static DateTime GetDate()
        {
            do
            {
                Console.WriteLine("Date Format: yyyy-MM-dd HH:mm:ss");
                var temp = Console.ReadLine();
                if (temp == "back")
                    throw new Exception("Main menu");
                var regex = new Regex(@"([12][190][90123]\d)-([10]\d)-([0123]\d) ([012]\d):([0-6]\d):([0-6]\d)");
                if (regex.IsMatch(temp))
                    return DateTime.Parse(temp);
                Writer.WrongInput();
            } while (true);
        }
    }
}
