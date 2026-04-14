using System;
using TechFactory.Interfaces;

namespace TechFactory.Models
{
    public class IProneLaptop : ILaptop { public void GetInfo() => Console.WriteLine("IProne Laptop: Stylish and powerful."); }
    public class IProneNetbook : INetbook { public void GetInfo() => Console.WriteLine("IProne Netbook: Ultra-thin for work."); }
    public class IProneEBook : IEBook { public void GetInfo() => Console.WriteLine("IProne EBook: Retina display for reading."); }
    public class IProneSmartphone : ISmartphone { public void GetInfo() => Console.WriteLine("IProne Smartphone: The industry standard."); }
}