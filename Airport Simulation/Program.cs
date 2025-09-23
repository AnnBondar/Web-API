using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AirportSimulation
{
    class Passenger
    {
        public string Name { get; set; }
        public string FlightNumber { get; set; }
        public bool HasTicket { get; set; }
        public bool PassedSecurity { get; set; }
        public bool IsOnBoard { get; set; }
    }

    class Flight
    {
        public string FlightNumber { get; set; }
        public string Destination { get; set; }
        public int DepartureTime { get; set; } // in ticks
        public string Status { get; set; } = "OnTime"; // OnTime, Delayed, Boarding, Departed
        public int Capacity { get; set; }
        public List<Passenger> PassengersOnBoard { get; set; } = new();
    }

    class Airport
    {
        private List<Flight> flights = new();
        private List<Passenger> allPassengers = new();

        private Queue<Passenger> registrationQueue = new();
        private Queue<Passenger> securityQueue = new();

        private int time = 0;
        private Random rand = new();

        private int registrationDesks = 3;
        private int securityPoints = 2;
        private int boardingSpeed = 5;

        // boarding starts this many ticks before DepartureTime (implementation choice)
        private int boardingOffset = 3;

        public Airport()
        {
            flights.Add(new Flight { FlightNumber = "PS101", Destination = "Kyiv", DepartureTime = 10, Capacity = 50 });
            flights.Add(new Flight { FlightNumber = "LH202", Destination = "Berlin", DepartureTime = 15, Capacity = 30 });
            flights.Add(new Flight { FlightNumber = "FR303", Destination = "London", DepartureTime = 20, Capacity = 40 });
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Tick: {time} ===");

                GenerateNewPassenger();
                ProcessRegistration();
                ProcessSecurity();
                UpdateFlights();
                PrintStatus();

                time++;
                Thread.Sleep(1000); // 1 second = 1 tick (implementation mapping)
            }
        }

        private void GenerateNewPassenger()
        {
            // arrival probability (implementation choice) — kept simple
            if (rand.NextDouble() < 0.5)
            {
                if (flights.Count == 0) return;

                var flight = flights[rand.Next(flights.Count)];
                var passenger = new Passenger
                {
                    Name = "Passenger " + rand.Next(1000, 9999),
                    FlightNumber = flight.FlightNumber
                };
                allPassengers.Add(passenger);
                registrationQueue.Enqueue(passenger);
                Console.WriteLine($"[NEW] {passenger.Name} arrived for flight {flight.FlightNumber}");
            }
        }

        private void ProcessRegistration()
        {
            for (int i = 0; i < registrationDesks; i++)
            {
                if (registrationQueue.Count > 0)
                {
                    var passenger = registrationQueue.Dequeue();
                    passenger.HasTicket = true;
                    securityQueue.Enqueue(passenger);
                    Console.WriteLine($"[CHECK-IN] {passenger.Name} checked in and moved to security queue.");
                }
            }
        }

        private void ProcessSecurity()
        {
            for (int i = 0; i < securityPoints; i++)
            {
                if (securityQueue.Count > 0)
                {
                    var passenger = securityQueue.Dequeue();
                    passenger.PassedSecurity = true;
                    Console.WriteLine($"[SECURITY] {passenger.Name} passed security.");
                }
            }
        }

        private void UpdateFlights()
        {
            // iterate over a copy because we may remove flights
            foreach (var flight in flights.ToList())
            {
                // set boarding when it's time (implementation uses boardingOffset)
                if (time == flight.DepartureTime - boardingOffset && flight.Status == "OnTime")
                {
                    flight.Status = "Boarding";
                    Console.WriteLine($"[INFO] Flight {flight.FlightNumber} started boarding.");
                }
                // departure moment
                else if (time == flight.DepartureTime)
                {
                    flight.Status = "Departed";
                    Console.WriteLine($"[INFO] Flight {flight.FlightNumber} has departed!");

                    // Who missed the flight? those assigned to this flight but not on board
                    var missedPassengers = allPassengers
                        .Where(p => p.FlightNumber == flight.FlightNumber && !p.IsOnBoard)
                        .ToList();

                    foreach (var p in missedPassengers)
                    {
                        Console.WriteLine($"[MISSED] {p.Name} missed flight {flight.FlightNumber}!");
                    }

                    // Remove only passengers who were on board (they flew)
                    allPassengers.RemoveAll(p => p.FlightNumber == flight.FlightNumber && p.IsOnBoard);

                    // Optionally clear flight's passenger list (not necessary if removing flight)
                    // flight.PassengersOnBoard.Clear();

                    // Remove the flight from active list
                    flights.Remove(flight);
                }

                // boarding process while status == Boarding
                if (flight.Status == "Boarding")
                {
                    var readyPassengers = allPassengers
                        .Where(p => p.FlightNumber == flight.FlightNumber && p.HasTicket && p.PassedSecurity && !p.IsOnBoard)
                        .Take(boardingSpeed)
                        .ToList();

                    foreach (var p in readyPassengers)
                    {
                        p.IsOnBoard = true;
                        flight.PassengersOnBoard.Add(p);
                        Console.WriteLine($"[BOARDING] {p.Name} boarded flight {flight.FlightNumber}");
                    }
                }
            }
        }

        private void PrintStatus()
        {
            Console.WriteLine("\n=== Flight Statuses ===");
            foreach (var flight in flights)
            {
                ConsoleColor color = ConsoleColor.White;
                if (flight.Status == "OnTime") color = ConsoleColor.Green;
                else if (flight.Status == "Boarding") color = ConsoleColor.Yellow;
                else if (flight.Status == "Departed") color = ConsoleColor.Red;
                else if (flight.Status == "Delayed") color = ConsoleColor.Cyan;

                Console.ForegroundColor = color;
                Console.WriteLine($"{flight.FlightNumber} to {flight.Destination} - {flight.Status} (Dep: {flight.DepartureTime})");
                Console.ResetColor();
            }

            Console.WriteLine($"\nCheck-in queue: {registrationQueue.Count}");
            Console.WriteLine($"Security queue: {securityQueue.Count}");
            Console.WriteLine($"Waiting in departure area: {allPassengers.Count(p => p.HasTicket && p.PassedSecurity && !p.IsOnBoard)}");
        }
    }

    class Program
    {
        static void Main()
        {
            Airport airport = new();
            airport.Run();
        }
    }
}