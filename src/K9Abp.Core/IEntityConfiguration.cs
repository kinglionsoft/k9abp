using Microsoft.EntityFrameworkCore;

namespace K9Abp.Core
{
    /// <summary>
    /// Used to config Entities in Plugin Modules
    /// </summary>
    public interface IEntityConfiguration
    {
        void Configure(ModelBuilder builder);
    }
}