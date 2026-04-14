using System;
using System.Text;
using BuilderTask.Builders;
using BuilderTask.Directors;
using BuilderTask.Interfaces;
using BuilderTask.Models;

namespace BuilderTask
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.WriteLine("RPG Character Creator");
            Console.WriteLine(new string('=', 30));

            Director director = new Director();

            ICharacterBuilder heroBuilder = new HeroBuilder();
            director.ConstructDreamHero(heroBuilder);
            
            heroBuilder.AddInventoryItem("Карта скарбів"); 
            Character dreamHero = heroBuilder.GetCharacter();

            ICharacterBuilder enemyBuilder = new EnemyBuilder();
            director.ConstructNemesis(enemyBuilder);
            Character nemesis = enemyBuilder.GetCharacter();

            dreamHero.ShowDetails();
            nemesis.ShowDetails();
        }
    }
}