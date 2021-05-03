using Gridnine.FlightCodingTest.Filter;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gridnine.FlightCodingTest.InOut
{
    public class Writer
    {
        public static void Greeting()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Hello World!");
            Console.ResetColor();
        }

        public static void WriteList(IEnumerable<Flight> flights)
        {
            if (!flights.Any())
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nEmpty list");
                Console.ForegroundColor = ConsoleColor.White;
            }
            flights.ToList().ForEach(x =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Flight with {x.Segments.Count} segments:");

                Console.WriteLine("Departure:\t\tArrival:");

                List<Segment> seg = x.Segments as List<Segment>;

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                seg.ForEach(s =>
                {
                    Console.WriteLine($"{s.DepartureDate}\t{s.ArrivalDate}");
                });
                Console.WriteLine();
            });
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WrongInput(string ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n!Wrong input!");
            Console.WriteLine(ex);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WrongInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n!Wrong input!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteColor(string message, ConsoleColor color)
        {
            var pieces = Regex.Split(message, @"(\[[^\]]*\])");

            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];

                if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    Console.ForegroundColor = color;
                    piece = piece.Substring(1, piece.Length - 2);
                }

                Console.Write(piece);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        public static void MainMenu()
        {
            string message = ("\n|Type \'[list]\' to print flights\n" +
                "|Type \'[filter]\' to customize filter\n" +
                "|Type \'[add]\' to add new flight\n" +
                "|Type \'[fill]\' to fill list from source\n" +
                "|Type \'[empty]\' to empty list\n" +
                "|Type \'[reset]\' to reset data\n" +
                "|Type \'[exit]\' to exit\n");
            WriteColor(message, ConsoleColor.DarkMagenta);

        }

        public static void NumberOfSegments()
        {
            Console.WriteLine("\nType number of segments (from 1 to 10)");
        }

        public static void Filter()
        {
            string message = $"\n|Type number of criteria\n" +
                $"|Criteria:\n" +
                $"|[1]. Depart before arrive\n" +
                $"|[2]. Long ground time ({LongGroundTimeFilter.LongTime} hours)\n" +
                $"|[3]. Multi segment\n" +
                $"|[4]. Past depart\n";
            WriteColor(message, ConsoleColor.DarkMagenta);
        }

        public static void IncludeOrExclude()
        {
            string message = "\n|Type number of option:\n" +
                "|[1]. Include flights with criteria in list\n" +
                "|[2]. Exclude flights with criteria from list\n" +
                "|[3]. Show flights with criteria\n" +
                "|[4]. Show flights without criteria\n";
            WriteColor(message, ConsoleColor.DarkMagenta);
        }
    }
}
