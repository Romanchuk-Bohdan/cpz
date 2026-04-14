using System;
using SubscriptionSystem.Factories;

namespace SubscriptionSystem
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Subscription Management System");
            Console.WriteLine(new string('=', 30));

            PurchasingPlatform website = new WebSite();
            PurchasingPlatform app = new MobileApp();
            PurchasingPlatform manager = new ManagerCall();

            website.ProcessPurchase("premium");
            app.ProcessPurchase("educational");
            manager.ProcessPurchase("domestic");
        }
    }
}