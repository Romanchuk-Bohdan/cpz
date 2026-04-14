using System;
using TechFactory.Interfaces;
using TechFactory.Factories;

namespace TechFactory
{
    class Program
    {
        static void ClientCode(IDeviceFactory factory)
        {
            var laptop = factory.CreateLaptop();
            var smartphone = factory.CreateSmartphone();

            laptop.GetInfo();
            smartphone.GetInfo();
            Console.WriteLine(new string('-', 20));
        }

        static void Main()
        {
            Console.WriteLine("Tech Production System");
            Console.WriteLine(new string('=', 30));

            Console.WriteLine("Ordering IProne line:");
            ClientCode(new IProneFactory());

            Console.WriteLine("Ordering Kiaomi line:");
            ClientCode(new KiaomiFactory());

            Console.WriteLine("Ordering Balaxy line:");
            ClientCode(new BalaxyFactory());
        }
    }
}