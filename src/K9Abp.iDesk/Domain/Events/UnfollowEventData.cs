using Abp.Events.Bus;

namespace K9Abp.iDesk.Domain.Events
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