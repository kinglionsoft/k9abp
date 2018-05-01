using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace K9Abp.iDeskCore.Work.Follower
{
    public class DeskworkFollower: Entity
    {
        public virtual int WorkId { get; set; }

        [ForeignKey("WorkId")]
        public virtual Deskwork Deskwork { get; set; }

        public int FollowerId { get; set; }

        [ForeignKey("FollowerId")]
        public virtual User Follower { get; set; }
    }
}