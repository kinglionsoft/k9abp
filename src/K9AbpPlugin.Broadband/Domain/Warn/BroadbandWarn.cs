using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace K9AbpPlugin.Broadband.Warn
{
    public class BroadbandWarn: Entity, IMustHaveOrganizationUnit, IMustHaveTenant
    {
        [Required]
        public long OrganizationUnitId { get; set; }
        [Required]
        public int TenantId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CountyId { get; set; }
        [Required]
        public int DistinctId { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
    }
}