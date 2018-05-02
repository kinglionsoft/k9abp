using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.AutoMapper;
using K9Abp.iDeskCore.Work;

namespace K9Abp.iDesk.Work.Dto
{
    [AutoMapTo(typeof(Deskwork))]
    public class WorkCreateInput
    {
        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public EWorkUrgency Urgency { get; set; }
        
        public long? RelatedWorkId { get; set; }

        [Required]
        public int TagId { get; set; }

        [Required]
        public virtual long CustomerId { get; set; }

        /// <summary>
        /// 时限，单位：小时
        /// </summary>
        [Required]
        public int TimeLimit { get; set; }

        [Required]
        public long ReceiverId { get; set; }
    }
}