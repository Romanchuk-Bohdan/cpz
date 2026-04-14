using System;
using System.Collections.Generic;
using SubscriptionSystem.Interfaces;

namespace SubscriptionSystem.Models
{
    public class DomesticSubscription : ISubscription
    {
        public decimal MonthlyFee => 9.99m;
        public int MinimumPeriodMonths => 1;
        public List<string> Features => new List<string> { "National News", "Local Shows", "Sports" };

        public void GetDetails()
        {
            Console.WriteLine($"Domestic: ${MonthlyFee}/mo, Min {MinimumPeriodMonths} month(s). Features: {string.Join(", ", Features)}");
        }
    }
}