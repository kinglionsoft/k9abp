using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using K9Abp.iDesk.Domain;
using K9Abp.iDesk.Domain.Events;

namespace K9Abp.iDesk.Application.EventHandlers
{
    public class WorkEventHandler: IAsyncEventHandler<FollowEventData>,
        IAsyncEventHandler<UnfollowEventData>, 
        IAsyncEventHandler<StepChangeEventData>,
        IAsyncEventHandler<WorkCloseEventData>,
        IAsyncEventHandler<WorkCompletionEventData>,
        ITransientDependency
    {
        public Task HandleEventAsync(FollowEventData eventData)
        {
            if (eventData.EventSource is Deskwork work)
            {

            }
            throw new System.NotImplementedException();
        }

        public Task HandleEventAsync(UnfollowEventData eventData)
        {
            if (eventData.EventSource is Deskwork work)
            {

            }
            throw new System.NotImplementedException();
        }

        public Task HandleEventAsync(StepChangeEventData eventData)
        {
            if(eventData.EventSource is Deskwork work)
            {
                if (work.Steps.Count == 1)
                {
                    // Creation
                    // Start warning for overtime
                }
            }

            throw new System.NotImplementedException();
        }

        public Task HandleEventAsync(WorkCloseEventData eventData)
        {
            if (eventData.EventSource is Deskwork work)
            {
                // stop warning for overtime
            }
            throw new System.NotImplementedException();
        }

        public Task HandleEventAsync(WorkCompletionEventData eventData)
        {
            if (eventData.EventSource is Deskwork work)
            {
                // stop warning for overtime
            }
            throw new System.NotImplementedException();
        }
    }
}