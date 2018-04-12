using Abp.Application.Services;
using K9Abp.Application.Dto;
using K9Abp.Application.Logging.Dto;

namespace K9Abp.Application.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}

