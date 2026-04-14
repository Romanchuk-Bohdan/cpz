using System;
using System.Collections.Generic;

namespace BuilderTask.Models
{
    public class Character
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public double Height { get; set; }
        public string BodyType { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string Clothing { get; set; }
        public List<string> Inventory { get; set; } = new List<string>();
        public List<string> Deeds { get; set; } = new List<string>();

        public void ShowDetails()
        {
            Console.WriteLine($"\n--- {Role.ToUpper()}: {Name} ---");
            Console.WriteLine($"Physique: {Height} cm, {BodyType}");
            Console.WriteLine($"Appearance: Hair - {HairColor}, Eyes - {EyeColor}");
            Console.WriteLine($"Clothing: {Clothing}");
            Console.WriteLine($"Inventory: {string.Join(", ", Inventory)}");
            Console.WriteLine("Deeds:");
            foreach (var deed in Deeds)
            {
                Console.WriteLine($"  - {deed}");
            }
        }
    }
}