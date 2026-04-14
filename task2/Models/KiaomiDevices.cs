using System;
using TechFactory.Interfaces;

namespace TechFactory.Models
{
    public class KiaomiLaptop : ILaptop { public void GetInfo() => Console.WriteLine("Kiaomi Laptop: High performance, fair price."); }
    public class KiaomiNetbook : INetbook { public void GetInfo() => Console.WriteLine("Kiaomi Netbook: Compact for students."); }
    public class KiaomiEBook : IEBook { public void GetInfo() => Console.WriteLine("Kiaomi EBook: Long-lasting battery."); }
    public class KiaomiSmartphone : ISmartphone { public void GetInfo() => Console.WriteLine("Kiaomi Smartphone: Top specs for everyone."); }
}