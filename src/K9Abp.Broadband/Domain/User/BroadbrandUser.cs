using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace K9Abp.Broadband.User
{
    public class BroadbrandUser: Entity, IMustHaveOrganizationUnit
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(11)]
        [Column(TypeName = "char(11)")]
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// 宽带资费
        /// </summary>
        [Required]
        public string Tariff { get; set; }

        /// <summary>
        /// 带宽
        /// </summary>
        [Required]
        public short Bandwidth { get; set; }

        /// <summary>
        /// 渠道组织Id
        /// </summary>
        [Required]
        public long OrganizationUnitId { get; set; }

        /// <summary>
        /// 分纤箱
        /// </summary>
        [StringLength(50)]
        public string FiberBox { get; set; }

        /// <summary>
        /// 入网时间
        /// </summary>
        [Required]
        public DateTime JoinTime { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        [Required]
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 近6月平均消费，单位：分
        /// </summary>
        public int Fee6 { get; set; }

        /// <summary>
        /// 平均流量
        /// </summary>
        public int Gprs { get; set; }

        /// <summary>
        /// 套外流量
        /// </summary>
        public int ExtraGprs { get; set; }

        /// <summary>
        /// 套外语音
        /// </summary>
        public int ExtraVoice { get; set; }

        /// <summary>
        /// 拍照档位
        /// </summary>
        [StringLength(50)]
        public string Spot { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public EBroadbandStatus Status { get; set; }

        /// <summary>
        /// 设备情况
        /// </summary>
        [Required]
        public EBroadbrandDeviceStatus DeviceStatus { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public JsonObject<Dictionary<string, string>> Extra { get; set; }
    }
}