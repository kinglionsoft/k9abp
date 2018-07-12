using System;
using System.Threading.Tasks;
using Abp.Logging;
using Abp.Threading.BackgroundWorkers;
using K9AbpPlugin.Broadband.User;
using Abp.Configuration;

namespace K9AbpPlugin.Broadband.Jobs
{
    public class WarnCheckScheduleWorker : ScheduleWorkerBase, IScheduleWorker
    {
        private readonly IBroadbandAppService _broadbandAppService;

        public WarnCheckScheduleWorker(IBroadbandAppService broadbandAppService)
        {
            _broadbandAppService = broadbandAppService;
        }

        public override string Name { get; } = "broadband_warn_checker";
        public override string Corn { get; } = HangfireCron.Daily(1);
        public override async Task DoWorkAsync()
        {
            Logger.Info("Starting WarnCheckScheduleWorker");
            try
            {
                var threshold = await this.SettingManager.GetSettingValueAsync<int>(BroadbandSettingProvider.WarnThreshold);
                Logger.Info($"WarnCheckScheduleWorker threshold: {threshold}");
                await _broadbandAppService.CheckWarningAsync(threshold);
                Logger.Info("Finished WarnCheckScheduleWorker");
            }
            catch (Exception e)
            {
                Logger.Error("Finished WarnCheckScheduleWorker with Error", e);
            }
        }
    }
}