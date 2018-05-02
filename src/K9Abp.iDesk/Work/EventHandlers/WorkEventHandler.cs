using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using K9Abp.iDeskCore.Work;

namespace K9Abp.iDesk.Work.EventHandlers
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
            throw new System.NotImplementedException();
        }

        public Task HandleEventAsync(WorkCloseEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleEventAsync(WorkCompletionEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}