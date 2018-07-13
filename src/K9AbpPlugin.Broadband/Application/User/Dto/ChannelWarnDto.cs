using System;
using System.Collections.Generic;
using Abp.AutoMapper;

namespace K9AbpPlugin.Broadband.User
{
    public class ChannelWarnDto
    {
        public long CountyId { get; set; }
        public string CountyName { get; set; }
        public long DistinctId { get; set; }
        public string DistinctName { get; set; }
        public long OrganizationUnitId { get; set; }
        public string OrganizationUnitName { get; set; }
        public List<WarnUserDto> Users { get; set; }
    }
    [AutoMapFrom(typeof(BroadbandUserDto))]
    public class WarnUserDto
    {
        public long Id { get; set; }
        public string Phone { get; set; }
        public string SpotProduct { get; set; }
        public int SpotBandwidth { get; set; }
        public DateTime ExpireTime { get; set; }
        public int Left => (int)(ExpireTime - DateTime.Today).TotalDays;
    }
}