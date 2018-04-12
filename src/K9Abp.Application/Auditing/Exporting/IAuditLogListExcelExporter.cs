using System.Collections.Generic;
using K9Abp.Application.Auditing.Dto;
using K9Abp.Application.Dto;

namespace K9Abp.Application.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}

