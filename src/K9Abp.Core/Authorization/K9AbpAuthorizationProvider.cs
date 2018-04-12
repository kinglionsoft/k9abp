using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace K9Abp.Core.Authorization
{
    public class K9AbpAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public K9AbpAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public K9AbpAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var administration = context.CreatePermission(PermissionNames.Administration, L("Administration"));

            var users = administration.CreateChildPermission(PermissionNames.Administration_Users, L("Users"));
            users.CreateChildPermission(PermissionNames.Administration_Users_Create, L("Create"));
            users.CreateChildPermission(PermissionNames.Administration_Users_Edit, L("Edit"));
            users.CreateChildPermission(PermissionNames.Administration_Users_Delete, L("Delete"));
            users.CreateChildPermission(PermissionNames.Administration_Users_Impersonation, L("Impersonation"));
            users.CreateChildPermission(PermissionNames.Administration_Users_ChangePermissions, L("ChangePermissions"));

            var roles = administration.CreateChildPermission(PermissionNames.Administration_Roles, L("Roles"));
            roles.CreateChildPermission(PermissionNames.Administration_Roles_Create, L("Create"));
            roles.CreateChildPermission(PermissionNames.Administration_Roles_Edit, L("Edit"));
            roles.CreateChildPermission(PermissionNames.Administration_Roles_Delete, L("Delete"));

            var languages = administration.CreateChildPermission(PermissionNames.Administration_Languages, L("Languages"));
            languages.CreateChildPermission(PermissionNames.Administration_Languages_Create, L("Create"));
            languages.CreateChildPermission(PermissionNames.Administration_Languages_Edit, L("Edit"));
            languages.CreateChildPermission(PermissionNames.Administration_Languages_Delete, L("Delete"));
            languages.CreateChildPermission(PermissionNames.Administration_Languages_ChangeTexts, L("ChangeTexts"));

            administration.CreateChildPermission(PermissionNames.Administration_AuditLogs, L("AuditLogs"));

            var ou = administration.CreateChildPermission(PermissionNames.Administration_OrganizationUnits, L("OrganizationUnits"));
            ou.CreateChildPermission(PermissionNames.Administration_OrganizationUnits_ManageOrganizationTree, L("ManageOrganizationTree"));
            ou.CreateChildPermission(PermissionNames.Administration_OrganizationUnits_ManageMembers, L("ManageMembers"));

            administration.CreateChildPermission(PermissionNames.Administration_HangfireDashboard, L("HangfireDashboard"));

            administration.CreateChildPermission(PermissionNames.Administration_UiCustomization, L("UiCustomization"));

            // TENANT-SPECIFIC PERMISSIONS
            context.CreatePermission(PermissionNames.Tenant_Dashboard, L("Dashboard"));

            var administrationTenant = administration.CreateChildPermission(PermissionNames.Administration_Tenant, L("Tenant"));
            administrationTenant.CreateChildPermission(PermissionNames.Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administrationTenant.CreateChildPermission(PermissionNames.Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            // HOST-SPECIFIC PERMISSIONS
            var editions = context.CreatePermission(PermissionNames.Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(PermissionNames.Editions_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(PermissionNames.Editions_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(PermissionNames.Editions_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);

            var tenants = context.CreatePermission(PermissionNames.Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            users.CreateChildPermission(PermissionNames.Tenants_Create, L("Create"), multiTenancySides: MultiTenancySides.Host);
            users.CreateChildPermission(PermissionNames.Tenants_Edit, L("Edit"), multiTenancySides: MultiTenancySides.Host);
            users.CreateChildPermission(PermissionNames.Tenants_Delete, L("Delete"), multiTenancySides: MultiTenancySides.Host);
            users.CreateChildPermission(PermissionNames.Tenants_Impersonation, L("Impersonation"), multiTenancySides: MultiTenancySides.Host);
            users.CreateChildPermission(PermissionNames.Tenants_ChangeFeatures, L("ChangeFeatures"), multiTenancySides: MultiTenancySides.Host);

            var host = administration.CreateChildPermission(PermissionNames.Administration_Host, L("Host"), multiTenancySides: MultiTenancySides.Host);
            host.CreateChildPermission(PermissionNames.Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: MultiTenancySides.Host);
            host.CreateChildPermission(PermissionNames.Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            host.CreateChildPermission(PermissionNames.Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, K9AbpConsts.LocalizationSourceName);
        }
    }
}

