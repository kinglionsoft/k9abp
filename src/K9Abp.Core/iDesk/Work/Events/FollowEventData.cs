using Abp.Events.Bus;

namespace K9Abp.iDeskCore.Work
{
    public class FollowEventData: EventData
    {
        public long FollwerId { get; }

        public FollowEventData(long follwerId)
        {
            FollwerId = follwerId;
        }
    }
}