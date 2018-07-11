using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace K9AbpPlugin.Broadband.User
{
    [AutoMapFrom(typeof(BroadbandUserDto))]
    public class WarnDto
    {
        public string Phone { get; set; }
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public int DistinctId { get; set; }
        public string DistinctName { get; set; }
        public string OrganizationUnitName { get; set; }
        /// <summary>
        /// 拍照套餐
        /// </summary>
        public string SpotProduct { get; set; }
        /// <summary>
        /// 拍照带宽
        /// </summary>
        public int SpotBandwidth { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}