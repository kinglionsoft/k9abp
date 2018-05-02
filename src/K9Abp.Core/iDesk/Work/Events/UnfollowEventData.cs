using Abp.Events.Bus;

namespace K9Abp.iDeskCore.Work
{
    public class UnfollowEventData: EventData
    {
        public long FollwerId { get; }

        public UnfollowEventData(long follwerId)
        {
            FollwerId = follwerId;
        }
    }
}