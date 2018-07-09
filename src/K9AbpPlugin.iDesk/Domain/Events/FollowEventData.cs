using Abp.Events.Bus;

namespace K9Abp.iDesk.Domain.Events
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