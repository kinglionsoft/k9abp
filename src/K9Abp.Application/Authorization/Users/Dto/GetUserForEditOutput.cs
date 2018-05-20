using System;
using System.Collections.Generic;
using K9Abp.Application.Organizations.Dto;

namespace K9Abp.Application.Authorization.Users.Dto
{
    public class GetUserForEditOutput
    {
        public Guid? ProfilePictureId { get; set; }

        public UserEditDto User { get; set; }

        public IList<string> Roles { get; set; }

        // public List<OrganizationUnitDto> AllOrganizationUnits { get; set; }

        public IList<long> MemberedOrganizationUnits { get; set; }
    }
}
