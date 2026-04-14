using System;
using System.Collections.Generic;

namespace VirusPrototype
{
    public class Virus
    {
        public double Weight { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public List<Virus> Children { get; set; }

        public Virus(double weight, int age, string name, string species)
        {
            Weight = weight;
            Age = age;
            Name = name;
            Species = species;
            Children = new List<Virus>();
        }

        public Virus Clone()
        {
            Virus clone = new Virus(Weight, Age, Name, Species);
            
            foreach (var child in Children)
            {
                clone.Children.Add(child.Clone());
            }
            
            return clone;
        }

        public void Display(int indent = 0)
        {
            string indentString = new string('-', indent);
            Console.WriteLine($"{indentString}> {Name} ({Species}) - Age: {Age}, Weight: {Weight}");
            
            foreach (var child in Children)
            {
                child.Display(indent + 2);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Virus Family Cloning System");
            Console.WriteLine(new string('=', 30));

            Virus child1 = new Virus(1.5, 1, "C-1", "Alpha");
            Virus child2 = new Virus(1.2, 1, "C-2", "Alpha");
            Virus child3 = new Virus(1.8, 1, "C-3", "Alpha");

            Virus parent1 = new Virus(3.5, 5, "P-1", "Alpha");
            parent1.Children.Add(child1);
            parent1.Children.Add(child2);

            Virus parent2 = new Virus(3.0, 4, "P-2", "Alpha");
            parent2.Children.Add(child3);

            Virus grandParent = new Virus(5.5, 10, "GP-1", "Alpha");
            grandParent.Children.Add(parent1);
            grandParent.Children.Add(parent2);

            Virus clonedFamily = grandParent.Clone();

            grandParent.Name = "GP-1 (MUTATED)";
            parent1.Children[0].Name = "C-1 (MUTATED)";

            Console.WriteLine("ORIGINAL FAMILY:");
            grandParent.Display();

            Console.WriteLine("\nCLONED FAMILY (Proof of Deep Copy):");
            clonedFamily.Display();
        }
    }
}