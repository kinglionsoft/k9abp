﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using K9Abp.Application.MultiTenancy.HostDashboard.Dto;
using K9Abp.Core;
using K9Abp.Core.MultiTenancy.Payments;

namespace K9Abp.Application.MultiTenancy.HostDashboard
{
    public class IncomeStatisticsService : K9AbpDomainServiceBase, IIncomeStatisticsService
    {
        private readonly IRepository<SubscriptionPayment, long> _subscriptionPaymentRepository;

        public IncomeStatisticsService(IRepository<SubscriptionPayment, long> subscriptionPaymentRepository)
        {
            _subscriptionPaymentRepository = subscriptionPaymentRepository;
        }

        private async Task<List<IncomeStastistic>> GetDailyIncomeStatisticsData(DateTime startDate, DateTime endDate)
        {
            var dailyRecords = await _subscriptionPaymentRepository.GetAll()
                .Where(s => s.CreationTime >= startDate &&
                            s.CreationTime <= endDate &&
                            s.Status == SubscriptionPaymentStatus.Completed)
                .GroupBy(s => new DateTime(s.CreationTime.Year, s.CreationTime.Month, s.CreationTime.Day))
                .Select(s => new IncomeStastistic
                {
                    Date = s.First().CreationTime.Date,
                    Amount = s.Sum(c => c.Amount)
                })
                .ToListAsync();

            FillGapsInDailyIncomeStatistics(dailyRecords, startDate, endDate);
            return dailyRecords.OrderBy(s => s.Date).ToList();
        }

        private static void FillGapsInDailyIncomeStatistics(ICollection<IncomeStastistic> dailyRecords, DateTime startDate, DateTime endDate)
        {
            var currentDay = startDate;
            while (currentDay <= endDate)
            {
                if (dailyRecords.All(d => d.Date != currentDay.Date))
                {
                    dailyRecords.Add(new IncomeStastistic(currentDay));
                }

                currentDay = currentDay.AddDays(1);
            }
        }

        public async Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval)
        {
            List<IncomeStastistic> incomeStastistics;

            switch (dateInterval)
            {
                case ChartDateInterval.Daily:
                    incomeStastistics = await GetDailyIncomeStatisticsData(startDate, endDate);
                    break;
                case ChartDateInterval.Weekly:
                    incomeStastistics = await GetWeeklyIncomeStatisticsData(startDate, endDate);
                    break;
                case ChartDateInterval.Monthly:
                    incomeStastistics = await GetMonthlyIncomeStatisticsData(startDate, endDate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dateInterval), dateInterval, null);
            }

            incomeStastistics.ForEach(i =>
            {
                i.Label = i.Date.ToString(L("DateFormatShort"));
            });

            return incomeStastistics.OrderBy(i => i.Date).ToList();
        }

        private async Task<List<IncomeStastistic>> GetWeeklyIncomeStatisticsData(DateTime startDate, DateTime endDate)
        {
            var dailyRecords = await GetDailyIncomeStatisticsData(startDate, endDate);
            var firstDayOfWeek = DateTimeFormatInfo.CurrentInfo == null
                ? DayOfWeek.Sunday
                : DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;

            var incomeStastistics = new List<IncomeStastistic>();
            decimal weeklyAmount = 0;
            var weekStart = dailyRecords.First().Date;
            var isFirstWeek = weekStart.DayOfWeek == firstDayOfWeek;

            dailyRecords.ForEach(dailyRecord =>
            {
                if (dailyRecord.Date.DayOfWeek == firstDayOfWeek)
                {
                    if (!isFirstWeek)
                    {
                        incomeStastistics.Add(new IncomeStastistic(weekStart, weeklyAmount));
                    }

                    isFirstWeek = false;
                    weekStart = dailyRecord.Date;
                    weeklyAmount = 0;
                }

                weeklyAmount += dailyRecord.Amount;
            });

            incomeStastistics.Add(new IncomeStastistic(weekStart, weeklyAmount));
            return incomeStastistics;
        }

        private async Task<List<IncomeStastistic>> GetMonthlyIncomeStatisticsData(DateTime startDate, DateTime endDate)
        {
            var dailyRecords = await GetDailyIncomeStatisticsData(startDate, endDate);
            var query = dailyRecords.GroupBy(d => new
            {
                d.Date.Year,
                d.Date.Month
            })
            .Select(grouping => new IncomeStastistic
            {
                Date = FindMonthlyDate(startDate, grouping.Key.Year, grouping.Key.Month),
                Amount = grouping.DefaultIfEmpty().Sum(x => x.Amount)
            });

            return query.ToList();
        }

        private static DateTime FindMonthlyDate(DateTime startDate, int groupYear, int groupMonth)
        {
            if (groupYear == startDate.Year && groupMonth == startDate.Month)
            {
                return new DateTime(groupYear, groupMonth, startDate.Day);
            }

            return new DateTime(groupYear, groupMonth, 1);
        }
    }
}
