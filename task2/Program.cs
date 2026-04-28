using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Mediator
{
    class CommandCentre
    {
        private List<Runway> _runways = new List<Runway>();
        private List<Aircraft> _aircrafts = new List<Aircraft>();

        public CommandCentre(Runway[] runways, Aircraft[] aircrafts)
        {
            this._runways.AddRange(runways);
            this._aircrafts.AddRange(aircrafts);

            foreach (var r in _runways)
            {
                r.SetMediator(this);
            }

            foreach (var a in _aircrafts)
            {
                a.SetMediator(this);
            }
        }

        public void RequestLanding(Aircraft aircraft)
        {
            Console.WriteLine("Checking runway.");
            var freeRunway = _runways.FirstOrDefault(r => !r.IsBusy);

            if (freeRunway != null)
            {
                freeRunway.IsBusy = true;
                aircraft.CurrentRunwayId = freeRunway.Id;
                Console.WriteLine($"Aircraft {aircraft.Name} has landed.");
                freeRunway.HighLightRed();
            }
            else
            {
                Console.WriteLine("Could not land, the runway is busy.");
            }
        }

        public void RequestTakeOff(Aircraft aircraft)
        {
            if (aircraft.CurrentRunwayId.HasValue)
            {
                var runway = _runways.FirstOrDefault(r => r.Id == aircraft.CurrentRunwayId.Value);
                if (runway != null)
                {
                    runway.IsBusy = false;
                    aircraft.CurrentRunwayId = null;
                    runway.HighLightGreen();
                    Console.WriteLine($"Aircraft {aircraft.Name} has took off.");
                }
            }
        }
    }

    class Aircraft
    {
        public string Name;
        public Guid? CurrentRunwayId { get; set; }
        public bool IsTakingOff { get; set; }
        
        private CommandCentre _commandCentre;

        public Aircraft(string name, int size)
        {
            this.Name = name;
        }

        public void SetMediator(CommandCentre commandCentre)
        {
            _commandCentre = commandCentre;
        }

        public void Land()
        {
            Console.WriteLine($"Aircraft {this.Name} is landing.");
            _commandCentre?.RequestLanding(this);
        }

        public void TakeOff()
        {
            Console.WriteLine($"Aircraft {this.Name} is taking off.");
            IsTakingOff = true;
            _commandCentre?.RequestTakeOff(this);
            IsTakingOff = false;
        }
    }

    class Runway
    {
        public readonly Guid Id = Guid.NewGuid();
        public bool IsBusy { get; set; }
        
        private CommandCentre _commandCentre;

        public void SetMediator(CommandCentre commandCentre)
        {
            _commandCentre = commandCentre;
        }

        public void HighLightRed()
        {
            Console.WriteLine($"Runway {this.Id} is busy!");
        }

        public void HighLightGreen()
        {
            Console.WriteLine($"Runway {this.Id} is free!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Runway[] runways = { new Runway(), new Runway() };
            Aircraft[] aircrafts = 
            { 
                new Aircraft("Boeing 747", 100), 
                new Aircraft("Airbus A320", 80), 
                new Aircraft("Cessna 172", 20) 
            };

            CommandCentre commandCentre = new CommandCentre(runways, aircrafts);

            aircrafts[0].Land();
            Console.WriteLine();

            aircrafts[1].Land();
            Console.WriteLine();

            aircrafts[2].Land();
            Console.WriteLine();

            aircrafts[0].TakeOff();
            Console.WriteLine();

            aircrafts[2].Land();
        }
    }
}