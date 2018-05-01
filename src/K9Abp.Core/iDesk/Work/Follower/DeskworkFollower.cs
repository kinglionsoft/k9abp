using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace K9Abp.iDeskCore.Work.Follower
{
    public class DeskworkFollower: Entity
    {
        [Required]
        public virtual int WorkId { get; set; }

        [ForeignKey("WorkId")]
        public virtual Deskwork Deskwork { get; set; }

        [Required]
        public virtual int FollowerId { get; set; }

        [ForeignKey("FollowerId")]
        public virtual Core.Authorization.Users.User Follower { get; set; }
    }
}