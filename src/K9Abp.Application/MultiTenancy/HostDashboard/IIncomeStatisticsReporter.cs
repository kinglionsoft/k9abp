using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using K9Abp.Application.MultiTenancy.HostDashboard.Dto;

namespace K9Abp.Application.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}
