namespace K9Abp.Core
{
    public class K9AbpConsts
    {
        public const string LocalizationSourceName = "yk";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;

        public const string DefaultLanguae = "zh-CN";

        public const int PaymentCacheDurationInMinutes = 30;

        public const int MaxPhoneNumberLength = 11;

        public const string TenancyNameRegex = "^[a-zA-Z][a-zA-Z0-9_-]{1,}$";

        public const int MaxNameLength = 128;

        public const string NA = "N/A";

        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public const string DefaultPassPhrase = "gsKxGZ012HLL3MI5";

        public static class Menu
        {
            public static class Common
            {
                public const string Administration = "Administration";
                public const string Roles = "Administration.Roles";
                public const string Users = "Administration.Users";
                public const string AuditLogs = "Administration.AuditLogs";
                public const string OrganizationUnits = "Administration.OrganizationUnits";
                public const string Languages = "Administration.Languages";
                public const string DemoUiComponents = "Administration.DemoUiComponents";
                public const string UiCustomization = "Administration.UiCustomization";
                public const string Workbench = "Workbench";
            }

            public static class Host
            {
                public const string Tenants = "Tenants";
                public const string Editions = "Editions";
                public const string Maintenance = "Administration.Maintenance";
                public const string Settings = "Administration.Settings.Host";
                public const string Dashboard = "Dashboard";
            }

            public static class Tenant
            {
                public const string Dashboard = "Dashboard.Tenant";
                public const string Settings = "Administration.Settings.Tenant";
                public const string SubscriptionManagement = "Administration.SubscriptionManagement.Tenant";
            }
        }
    }
}

