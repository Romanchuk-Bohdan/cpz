using System;
using System.Text;

public abstract class Hero
{
    public string Name { get; protected set; }
    
    public abstract string GetDescription();
    public abstract int GetPower();
}

public class Warrior : Hero
{
    public Warrior(string name)
    {
        Name = name;
    }

    public override string GetDescription()
    {
        return $"Воїн {Name}";
    }

    public override int GetPower()
    {
        return 100;
    }
}

public class Mage : Hero
{
    public Mage(string name)
    {
        Name = name;
    }

    public override string GetDescription()
    {
        return $"Маг {Name}";
    }

    public override int GetPower()
    {
        return 50;
    }
}

public class Paladin : Hero
{
    public Paladin(string name)
    {
        Name = name;
    }

    public override string GetDescription()
    {
        return $"Паладин {Name}";
    }

    public override int GetPower()
    {
        return 80;
    }
}

public abstract class InventoryDecorator : Hero
{
    protected Hero _hero;

    public InventoryDecorator(Hero hero)
    {
        _hero = hero;
    }
}

public class Weapon : InventoryDecorator
{
    private readonly string _weaponName;
    private readonly int _damage;

    public Weapon(Hero hero, string weaponName, int damage) : base(hero)
    {
        _weaponName = weaponName;
        _damage = damage;
    }

    public override string GetDescription()
    {
        return $"{_hero.GetDescription()}, Зброя: {_weaponName}";
    }

    public override int GetPower()
    {
        return _hero.GetPower() + _damage;
    }
}

public class Clothing : InventoryDecorator
{
    private readonly string _clothingName;
    private readonly int _defense;

    public Clothing(Hero hero, string clothingName, int defense) : base(hero)
    {
        _clothingName = clothingName;
        _defense = defense;
    }

    public override string GetDescription()
    {
        return $"{_hero.GetDescription()}, Одяг: {_clothingName}";
    }

    public override int GetPower()
    {
        return _hero.GetPower() + _defense;
    }
}

public class Artifact : InventoryDecorator
{
    private readonly string _artifactName;
    private readonly int _magicPower;

    public Artifact(Hero hero, string artifactName, int magicPower) : base(hero)
    {
        _artifactName = artifactName;
        _magicPower = magicPower;
    }

    public override string GetDescription()
    {
        return $"{_hero.GetDescription()}, Артефакт: {_artifactName}";
    }

    public override int GetPower()
    {
        return _hero.GetPower() + _magicPower;
    }
}

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        
        Hero warrior = new Warrior("Іван");
        Console.WriteLine(warrior.GetDescription());
        Console.WriteLine($"Базова сила: {warrior.GetPower()}\n");
        
        warrior = new Weapon(warrior, "Дворучний меч", 45);
        warrior = new Clothing(warrior, "Сталева броня", 60);
        warrior = new Artifact(warrior, "Амулет берсерка", 15);

        Console.WriteLine(warrior.GetDescription());
        Console.WriteLine($"Сила після екіпірування: {warrior.GetPower()}\n");

        Console.WriteLine(new string('-', 50) + "\n");

        Hero mage = new Mage("Мерлін");
        mage = new Weapon(mage, "Посох вічності", 20);
        mage = new Clothing(mage, "Мантія невидимості", 15);
        mage = new Artifact(mage, "Кільце вогню", 35);
        mage = new Artifact(mage, "Кільце льоду", 35); 

        Console.WriteLine(mage.GetDescription());
        Console.WriteLine($"Сила після екіпірування: {mage.GetPower()}");
    }
}