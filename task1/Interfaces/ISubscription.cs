using System.Collections.Generic;

namespace SubscriptionSystem.Interfaces
{
    public interface ISubscription
    {
        decimal MonthlyFee { get; }
        int MinimumPeriodMonths { get; }
        List<string> Features { get; }
        void GetDetails();
    }
}