using System.Threading.Tasks;
using Abp.Dependency;

namespace Abp.Threading.BackgroundWorkers
{
    public interface IScheduleWorker: ITransientDependency
    {
        string Name { get; }
        string Corn { get; }
        Task DoWorkAsync();
    }
}