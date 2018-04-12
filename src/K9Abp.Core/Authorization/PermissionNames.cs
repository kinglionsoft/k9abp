namespace K9Abp.Core.Authorization
{
    public static class PermissionNames
    {
        public const string DemoUiComponents = "DemoUiComponents";
        public const string Administration = "Administration";

        public const string Administration_Roles = "Administration.Roles";
        public const string Administration_Roles_Create = "Administration.Roles.Create";
        public const string Administration_Roles_Edit = "Administration.Roles.Edit";
        public const string Administration_Roles_Delete = "Administration.Roles.Delete";

        public const string Administration_Users = "Administration.Users";
        public const string Administration_Users_Create = "Administration.Users.Create";
        public const string Administration_Users_Edit = "Administration.Users.Edit";
        public const string Administration_Users_Delete = "Administration.Users.Delete";
        public const string Administration_Users_ChangePermissions = "Administration.Users.ChangePermissions";
        public const string Administration_Users_Impersonation = "Administration.Users.Impersonation";

        public const string Administration_Languages = "Administration.Languages";
        public const string Administration_Languages_Create = "Administration.Languages.Create";
        public const string Administration_Languages_Edit = "Administration.Languages.Edit";
        public const string Administration_Languages_Delete = "Administration.Languages.Delete";
        public const string Administration_Languages_ChangeTexts = "Administration.Languages.ChangeTexts";

        public const string Administration_AuditLogs = "Administration.AuditLogs";

        public const string Administration_OrganizationUnits = "Administration.OrganizationUnits";
        public const string Administration_OrganizationUnits_ManageOrganizationTree = "Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Administration_OrganizationUnits_ManageMembers = "Administration.OrganizationUnits.ManageMembers";

        public const string Administration_HangfireDashboard = "Administration.HangfireDashboard";

        public const string Administration_UiCustomization = "Administration.UiCustomization";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Tenant_Dashboard = "Tenant.Dashboard";

        public const string Administration_Tenant = "Administration.Tenant";
        public const string Administration_Tenant_Settings = "Administration.Tenant.Settings";
        public const string Administration_Tenant_SubscriptionManagement = "Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Editions = "Editions";
        public const string Editions_Create = "Editions.Create";
        public const string Editions_Edit = "Editions.Edit";
        public const string Editions_Delete = "Editions.Delete";

        public const string Tenants = "Tenants";
        public const string Tenants_Create = "Tenants.Create";
        public const string Tenants_Edit = "Tenants.Edit";
        public const string Tenants_ChangeFeatures = "Tenants.ChangeFeatures";
        public const string Tenants_Delete = "Tenants.Delete";
        public const string Tenants_Impersonation = "Tenants.Impersonation";

        public const string Administration_Host = "Administration.Host";
        public const string Administration_Host_Maintenance = "Administration.Host.Maintenance";
        public const string Administration_Host_Settings = "Administration.Host.Settings";
        public const string Administration_Host_Dashboard = "Administration.Host.Dashboard";
    }
}

