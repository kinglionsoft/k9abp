using System.Collections.Generic;
using K9Abp.Application.Authorization.Users.Dto;
using K9Abp.Application.Dto;

namespace K9Abp.Application.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}
