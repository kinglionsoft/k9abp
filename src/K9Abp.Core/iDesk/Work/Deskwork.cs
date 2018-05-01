using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using K9Abp.iDeskCore.Work.Customer;
using K9Abp.iDeskCore.Work.Step;

namespace K9Abp.iDeskCore.Work
{
    [Audited]
    public class Deskwork : AuditedAggregateRoot, IPassivable
    {
        #region IPassivable
        [Required]
        public virtual bool IsActive { get; set; }
        #endregion

        #region Properties

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public virtual EWorkUrgency Urgency { get; set; }

        [Required]
        public virtual EWorkStatus Status { get; set; }

        public int? RelatedWorkId { get; set; }

        [Required]
        public virtual int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual DeskworkCustomer Customer { get; set; }

        [Required]
        public EWorkCompletion Completion { get; set; }

        public virtual IList<DeskworkStep> Steps { get; set; }    

        #endregion
    }
}