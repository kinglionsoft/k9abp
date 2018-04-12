using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ORS.AspNetZeroCore
{
    public class AspNetZeroCoreModule: AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AspNetZeroCoreModule).GetAssembly());
        }
    }
}



