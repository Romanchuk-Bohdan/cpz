using System;

namespace SupportSystem
{
    public interface ISupportHandler
    {
        ISupportHandler SetNext(ISupportHandler handler);
        bool Handle();
    }

    public abstract class BaseSupportHandler : ISupportHandler
    {
        private ISupportHandler _nextHandler;

        public ISupportHandler SetNext(ISupportHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual bool Handle()
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle();
            }
            return false;
        }
    }

    public class BotHandler : BaseSupportHandler
    {
        public override bool Handle()
        {
            Console.WriteLine("\n[Рівень 1] Автоматичний бот-помічник");
            Console.WriteLine("1 - Дізнатися баланс рахунку");
            Console.WriteLine("2 - Дізнатися свій тарифний план");
            Console.WriteLine("3 - Інше (перейти до технічної підтримки 1-ї лінії)");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Ваш баланс: 150 грн. Дякуємо за звернення!");
                return true;
            }
            if (choice == "2")
            {
                Console.WriteLine("Ваш тариф: 'Супер Безліміт'. Дякуємо за звернення!");
                return true;
            }
            if (choice == "3")
            {
                return base.Handle();
            }

            Console.WriteLine("Невірний ввід, перенаправляємо далі...");
            return base.Handle();
        }
    }

    public class TechSupportLevel1Handler : BaseSupportHandler
    {
        public override bool Handle()
        {
            Console.WriteLine("\n[Рівень 2] Технічна підтримка 1-ї лінії");
            Console.WriteLine("1 - Немає доступу до інтернету");
            Console.WriteLine("2 - Поганий мобільний зв'язок");
            Console.WriteLine("3 - Інше (перейти до експертів 2-ї лінії)");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Спробуйте перезавантажити пристрій. Налаштування APN надіслано в SMS.");
                return true;
            }
            if (choice == "2")
            {
                Console.WriteLine("Ми зафіксували проблему зі зв'язком у вашому регіоні, інженери вже працюють.");
                return true;
            }
            if (choice == "3")
            {
                return base.Handle();
            }

            Console.WriteLine("Невірний ввід, перенаправляємо далі...");
            return base.Handle();
        }
    }

    public class TechSupportLevel2Handler : BaseSupportHandler
    {
        public override bool Handle()
        {
            Console.WriteLine("\n[Рівень 3] Технічна підтримка 2-ї лінії (Експерти)");
            Console.WriteLine("1 - Заміна SIM-картки");
            Console.WriteLine("2 - Блокування втраченого номера");
            Console.WriteLine("3 - Інше (з'єднати з оператором)");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Зверніться до найближчого магазину з паспортом для безкоштовної заміни SIM-картки.");
                return true;
            }
            if (choice == "2")
            {
                Console.WriteLine("Ваш номер тимчасово заблоковано для безпеки.");
                return true;
            }
            if (choice == "3")
            {
                return base.Handle();
            }

            Console.WriteLine("Невірний ввід, перенаправляємо далі...");
            return base.Handle();
        }
    }

    public class OperatorHandler : BaseSupportHandler
    {
        public override bool Handle()
        {
            Console.WriteLine("\n[Рівень 4] З'єднання з оператором");
            Console.WriteLine("1 - Поговорити з живим спеціалістом");
            Console.WriteLine("2 - Відмовитись (повернутись на головне меню)");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("З'єднуємо з оператором... Залишайтеся на лінії. Ваша позиція в черзі: 1.");
                return true;
            }

            return base.Handle();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var bot = new BotHandler();
            var techL1 = new TechSupportLevel1Handler();
            var techL2 = new TechSupportLevel2Handler();
            var humanOperator = new OperatorHandler();

            bot.SetNext(techL1).SetNext(techL2).SetNext(humanOperator);

            bool isResolved = false;

            while (!isResolved)
            {
                Console.WriteLine("\n=== ВІТАЄМО У СЛУЖБІ ПІДТРИМКИ ===");
                
                isResolved = bot.Handle();

                if (!isResolved)
                {
                    Console.WriteLine("\n*** Жоден рівень не зміг обробити ваш запит або ви скасували дію. ***");
                    Console.WriteLine("*** Меню розпочнеться спочатку. ***\n");
                }
            }

            Console.WriteLine("\nДякуємо за використання нашої системи. До побачення!");
        }
    }
}