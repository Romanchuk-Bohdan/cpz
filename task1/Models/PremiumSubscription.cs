using System;
using System.Collections.Generic;
using SubscriptionSystem.Interfaces;

namespace SubscriptionSystem.Models
{
    public class PremiumSubscription : ISubscription
    {
        public decimal MonthlyFee => 19.99m;
        public int MinimumPeriodMonths => 12;
        public List<string> Features => new List<string> { "All Channels", "4K Resolution", "No Ads", "Offline" };

        public void GetDetails()
        {
            Console.WriteLine($"Premium: ${MonthlyFee}/mo, Min {MinimumPeriodMonths} month(s). Features: {string.Join(", ", Features)}");
        }
    }
}