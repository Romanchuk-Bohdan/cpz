using System;
using SubscriptionSystem.Interfaces;
using SubscriptionSystem.Models;

namespace SubscriptionSystem.Factories
{
    public class MobileApp : PurchasingPlatform
    {
        public override ISubscription CreateSubscription(string type)
        {
            Console.WriteLine("Verifying via App Store / Google Play...");
            return type.ToLower() switch
            {
                "domestic" => new DomesticSubscription(),
                "educational" => new EducationalSubscription(),
                "premium" => new PremiumSubscription(),
                _ => throw new ArgumentException("Unknown type")
            };
        }
    }
}