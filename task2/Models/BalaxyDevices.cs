using System;
using TechFactory.Interfaces;

namespace TechFactory.Models
{
    public class BalaxyLaptop : ILaptop { public void GetInfo() => Console.WriteLine("Balaxy Laptop: Ultimate productivity."); }
    public class BalaxyNetbook : INetbook { public void GetInfo() => Console.WriteLine("Balaxy Netbook: Portable and smart."); }
    public class BalaxyEBook : IEBook { public void GetInfo() => Console.WriteLine("Balaxy EBook: Paper-like experience."); }
    public class BalaxySmartphone : ISmartphone { public void GetInfo() => Console.WriteLine("Balaxy Smartphone: Innovation in your hand."); }
}