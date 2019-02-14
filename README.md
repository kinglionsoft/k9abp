# 金涞ABP框架

## 主要功能

## 快速开始

### 1. MariaDB配置
* 使用Pomelo.EntityFrameworkCore.MySql，官方包（MySql.Data.EntityFrameworkCore）仍有问题，比如无法使用Guid.

### 2. 数据库迁移：

* 打开K9Abp.EntityFrameworkCore.csproj，将TargetFramework修改为netcoreapp2.0;
* 打开cmd，进入到K9Abp.EntityFrameworkCore.csproj所在目录，执行EF Core迁移指令，详见[https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
* 迁移完成后，将K9Abp.EntityFrameworkCore.csproj的TargetFramework修改为netstandard2.0.

### 3. 本地化
* 使用json文件进行本地化配置，语言文件根目录为 **/lang**
* 添加新增资源：yk
* 完善ABP内部的本地化资源(Abp、AbpWeb和AbpZero)未的汉化部分
* 支持i18n，统一配置到 ** src/K9Abp.Web.Host/lang** 。i18n不支持在自定义模块中新增的语言资源。
* 使用K9AbpUserConfiguration/GetAll替代原AbpUserConfiguration/GetAll接口（获取用户相关的系统配置数据）。

>  **待优化**：由于[AbpUserConfigurationBuilder](https://github.com/aspnetboilerplate/aspnetboilerplate/blob/3337d1e2d8e8e6225ed5c28020e16cdc5562cd99/src/Abp.Web.Common/Web/Configuration/AbpUserConfigurationBuilder.cs)不易扩展（没有接口不能重新实现、属性全是私有的不能继承、GetAll不是virtual)，所以重新写了K9AbpUserConfigurationBuilder和K9AbpUserConfigurationController，不够灵活。已经向ABP提了PR，将合并到3.5.0版本。

## 插件模块
ABP的模块可以是在startup module中进行显示依赖指定了，也可以按程序集的方式进行动态加载，详见[PlugIn Module](https://aspnetboilerplate.com/Pages/Documents/Module-System#plugin-modules)。

### 插件模块中的Entity
在使用EF时，需要在DbContext中定义Entity的DbSet<TEneity>，具体为：

``` c#
 public class K9AbpDbContext : AbpZeroDbContext<Tenant, Role, User, K9AbpDbContext>
{
	// TODO: Define an IDbSet for each entity of the application 
	public virtual DbSet<ProductDemo> ProductDemos { get; set; }

	public K9AbpDbContext(DbContextOptions<K9AbpDbContext> options)
		: base(options)
	{
	}
}
```
**讨论**：但使用插件模块时，可能存在本模块所独有的实体\聚合，其它模块不会引用，那么在插件模块中定义这些实体似乎更为合理，但也会导致插件模块需要依赖Microsoft.EntityFrameworkCore，从而不便于替换数据持久层。

### 编译事件脚本
在编译完成后，将dll复制到Plugins目录。
* 在项目属性 -> 生成事件 -> 后期生成事件命令行：

```bash
xcopy /E /Y /Q "$(TargetDir)$(TargetFileName)" ..\K9Abp.Web.Host\Plugins\
xcopy /E /Y /Q "$(TargetDir)$(TargetName).pdb" ..\K9Abp.Web.Host\Plugins\
```

## TODOs