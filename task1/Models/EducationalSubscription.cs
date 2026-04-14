using System;
using System.Collections.Generic;
using SubscriptionSystem.Interfaces;

namespace SubscriptionSystem.Models
{
    public class EducationalSubscription : ISubscription
    {
        public decimal MonthlyFee => 4.99m;
        public int MinimumPeriodMonths => 6;
        public List<string> Features => new List<string> { "Science Docs", "History Channel", "Kids Learning" };

        public void GetDetails()
        {
            Console.WriteLine($"Educational: ${MonthlyFee}/mo, Min {MinimumPeriodMonths} month(s). Features: {string.Join(", ", Features)}");
        }
    }
}