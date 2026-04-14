using System;
using SubscriptionSystem.Interfaces;
using SubscriptionSystem.Models;

namespace SubscriptionSystem.Factories
{
    public class ManagerCall : PurchasingPlatform
    {
        public override ISubscription CreateSubscription(string type)
        {
            Console.WriteLine("Creating subscription manually via CRM system...");
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