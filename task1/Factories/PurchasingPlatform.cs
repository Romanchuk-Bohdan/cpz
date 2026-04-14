using System;
using SubscriptionSystem.Interfaces;

namespace SubscriptionSystem.Factories
{
    public abstract class PurchasingPlatform
    {
        public abstract ISubscription CreateSubscription(string type);

        public void ProcessPurchase(string type)
        {
            ISubscription subscription = CreateSubscription(type);
            Console.WriteLine($"Platform: {this.GetType().Name}");
            subscription.GetDetails();
            Console.WriteLine(new string('-', 30));
        }
    }
}