using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace K9Abp.iDesk.Domain.Follower
{
    public class DeskworkFollower: Entity<long>
    {
        [Required]
        public virtual long WorkId { get; internal set; }

        [ForeignKey("WorkId")]
        public virtual Deskwork Deskwork { get; set; }

        [Required]
        public virtual long FollowerId { get; internal set; }

        [ForeignKey("FollowerId")]
        public virtual Core.Authorization.Users.User Follower { get; set; }
    }
}