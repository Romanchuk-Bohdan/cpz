using System;
using SubscriptionSystem.Interfaces;
using SubscriptionSystem.Models;

namespace SubscriptionSystem.Factories
{
    public class WebSite : PurchasingPlatform
    {
        public override ISubscription CreateSubscription(string type)
        {
            Console.WriteLine("Connecting to Web Payment Gateway...");
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