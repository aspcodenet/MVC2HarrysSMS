using System.Threading;

namespace HarrysSMSWeb.Services
{
    public class RiksbankenInterestService : IInterestService
    {
        public decimal GetBaseRate()
        {
            //Fake slow call
            Thread.Sleep(5000);
            return 5.1M;
        }
    }
}