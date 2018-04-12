﻿using System;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Runtime.Session;
using K9Abp.Application.Configuration.Dto;
using K9Abp.Core.Authorization;
using K9Abp.Core.Configuration;

namespace K9Abp.Application.Configuration
{
    [AbpAuthorize]
    public class UiCustomizationSettingsAppService : K9AbpAppServiceBase, IUiCustomizationSettingsAppService
    {
        private readonly SettingManager _settingManager;

        public UiCustomizationSettingsAppService(SettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task<UiCustomizationSettingsEditDto> GetUiManagementSettings()
        {
            return new UiCustomizationSettingsEditDto
            {
                Layout = new UiCustomizationLayoutSettingsEditDto
                {
                    LayoutType = await _settingManager.GetSettingValueAsync(AppSettings.UiManagement.LayoutType),
                    PageLoader = await _settingManager.GetSettingValueAsync(AppSettings.UiManagement.PageLoader),
                    ContentSkin = await _settingManager.GetSettingValueAsync(AppSettings.UiManagement.ContentSkin)
                },
                Header = new UiCustomizationHeaderSettingsEditDto
                {
                    DesktopFixedHeader = await _settingManager.GetSettingValueAsync<bool>(AppSettings.UiManagement.Header.DesktopFixedHeader),
                    DesktopMinimizeMode = await _settingManager.GetSettingValueAsync(AppSettings.UiManagement.Header.DesktopMinimizeMode),
                    MobileFixedHeader = await _settingManager.GetSettingValueAsync<bool>(AppSettings.UiManagement.Header.MobileFixedHeader),
                    DropdownSkinDesktop = await _settingManager.GetSettingValueAsync(AppSettings.UiManagement.Header.DropdownSkinDesktop),
                    DisplaySubmenuArrowDesktop = await _settingManager.GetSettingValueAsync<bool>(AppSettings.UiManagement.Header.DisplaySubmenuArrowDesktop),
                    DropdownSkin = await _settingManager.GetSettingValueAsync(AppSettings.UiManagement.Header.DropdownSkin)
                },
                Menu = new UiCustomizationMenuSettingsEditDto
                {
                    Position = await _settingManager.GetSettingValueAsync(AppSettings.UiManagement.LeftAside.Position),
                    AsideSkin = await _settingManager.GetSettingValueAsync(AppSettings.UiManagement.LeftAside.AsideSkin),
                    FixedAside = await _settingManager.GetSettingValueAsync<bool>(AppSettings.UiManagement.LeftAside.FixedAside),
                    AllowAsideMinimizing = await _settingManager.GetSettingValueAsync<bool>(AppSettings.UiManagement.LeftAside.AllowAsideMinimizing),
                    DefaultMinimizedAside = await _settingManager.GetSettingValueAsync<bool>(AppSettings.UiManagement.LeftAside.DefaultMinimizedAside),
                    AllowAsideHiding = await _settingManager.GetSettingValueAsync<bool>(AppSettings.UiManagement.LeftAside.AllowAsideHiding),
                    DefaultHiddenAside = await _settingManager.GetSettingValueAsync<bool>(AppSettings.UiManagement.LeftAside.DefaultHiddenAside),
                    SubmenuToggle = await _settingManager.GetSettingValueAsync(AppSettings.UiManagement.LeftAside.SubmenuToggle),
                    DropdownSubmenuSkin = await _settingManager.GetSettingValueAsync(AppSettings.UiManagement.LeftAside.DropdownSubmenuSkin),
                    DropdownSubmenuArrow = await _settingManager.GetSettingValueAsync<bool>(AppSettings.UiManagement.LeftAside.DropdownSubmenuArrow)
                },
                Footer = new UiCustomizationFooterSettingsEditDto()
                {
                    FixedFooter = await _settingManager.GetSettingValueAsync<bool>(AppSettings.UiManagement.Footer.FixedFooter)
                }
            };
        }

        public async Task UpdateUiManagementSettings(UiCustomizationSettingsEditDto settings)
        {
            await UpdateUserUiManagementSettingsAsync(settings);
        }

        public async Task UpdateDefaultUiManagementSettings(UiCustomizationSettingsEditDto settings)
        {
            if (await PermissionChecker.IsGrantedAsync(PermissionNames.Administration_Host_Settings))
            {
                await UpdateApplicationUiManagementSettingsAsync(settings);
            }
            else if (await PermissionChecker.IsGrantedAsync(PermissionNames.Administration_Tenant_Settings))
            {
                await UpdateTenantUiManagementSettingsAsync(settings);
            }
            else
            {
                throw new Exception("You cannot change default ui customization settings !");
            }
        }

        public async Task UseSystemDefaultSettings()
        {
            if (AbpSession.TenantId.HasValue)
            {
                await UpdateUserUiManagementSettingsAsync(await GetTenantUiCustomizationSettings());
            }
            else
            {
                await UpdateUserUiManagementSettingsAsync(await GetHostUiManagementSettings());
            }
        }

        private async Task<UiCustomizationSettingsEditDto> GetHostUiManagementSettings()
        {
            return new UiCustomizationSettingsEditDto
            {
                Layout = new UiCustomizationLayoutSettingsEditDto
                {
                    LayoutType = await _settingManager.GetSettingValueForApplicationAsync(AppSettings.UiManagement.LayoutType),
                    PageLoader = await _settingManager.GetSettingValueForApplicationAsync(AppSettings.UiManagement.PageLoader),
                    ContentSkin = await _settingManager.GetSettingValueForApplicationAsync(AppSettings.UiManagement.ContentSkin)
                },
                Header = new UiCustomizationHeaderSettingsEditDto
                {
                    DesktopFixedHeader = await _settingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.UiManagement.Header.DesktopFixedHeader),
                    DesktopMinimizeMode = await _settingManager.GetSettingValueForApplicationAsync(AppSettings.UiManagement.Header.DesktopMinimizeMode),
                    MobileFixedHeader = await _settingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.UiManagement.Header.MobileFixedHeader),
                    DropdownSkinDesktop = await _settingManager.GetSettingValueForApplicationAsync(AppSettings.UiManagement.Header.DropdownSkinDesktop),
                    DisplaySubmenuArrowDesktop = await _settingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.UiManagement.Header.DisplaySubmenuArrowDesktop),
                    DropdownSkin = await _settingManager.GetSettingValueForApplicationAsync(AppSettings.UiManagement.Header.DropdownSkin)
                },
                Menu = new UiCustomizationMenuSettingsEditDto
                {
                    Position = await _settingManager.GetSettingValueForApplicationAsync(AppSettings.UiManagement.LeftAside.Position),
                    AsideSkin = await _settingManager.GetSettingValueForApplicationAsync(AppSettings.UiManagement.LeftAside.AsideSkin),
                    FixedAside = await _settingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.UiManagement.LeftAside.FixedAside),
                    AllowAsideMinimizing = await _settingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.UiManagement.LeftAside.AllowAsideMinimizing),
                    DefaultMinimizedAside = await _settingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.UiManagement.LeftAside.DefaultMinimizedAside),
                    AllowAsideHiding = await _settingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.UiManagement.LeftAside.AllowAsideHiding),
                    DefaultHiddenAside = await _settingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.UiManagement.LeftAside.DefaultHiddenAside),
                    SubmenuToggle = await _settingManager.GetSettingValueForApplicationAsync(AppSettings.UiManagement.LeftAside.SubmenuToggle),
                    DropdownSubmenuSkin = await _settingManager.GetSettingValueForApplicationAsync(AppSettings.UiManagement.LeftAside.DropdownSubmenuSkin),
                    DropdownSubmenuArrow = await _settingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.UiManagement.LeftAside.DropdownSubmenuArrow)
                },
                Footer = new UiCustomizationFooterSettingsEditDto
                {
                    FixedFooter = await _settingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.UiManagement.Footer.FixedFooter)
                }
            };
        }

        private async Task<UiCustomizationSettingsEditDto> GetTenantUiCustomizationSettings()
        {
            var tenantId = AbpSession.GetTenantId();

            return new UiCustomizationSettingsEditDto
            {
                Layout = new UiCustomizationLayoutSettingsEditDto
                {
                    LayoutType = await _settingManager.GetSettingValueForTenantAsync(AppSettings.UiManagement.LayoutType, tenantId),
                    PageLoader = await _settingManager.GetSettingValueForTenantAsync(AppSettings.UiManagement.PageLoader, tenantId),
                    ContentSkin = await _settingManager.GetSettingValueForTenantAsync(AppSettings.UiManagement.ContentSkin, tenantId)
                },
                Header = new UiCustomizationHeaderSettingsEditDto
                {
                    DesktopFixedHeader = await _settingManager.GetSettingValueForTenantAsync<bool>(AppSettings.UiManagement.Header.DesktopFixedHeader, tenantId),
                    DesktopMinimizeMode = await _settingManager.GetSettingValueForTenantAsync(AppSettings.UiManagement.Header.DesktopMinimizeMode, tenantId),
                    MobileFixedHeader = await _settingManager.GetSettingValueForTenantAsync<bool>(AppSettings.UiManagement.Header.MobileFixedHeader, tenantId),
                    DropdownSkinDesktop = await _settingManager.GetSettingValueForTenantAsync(AppSettings.UiManagement.Header.DropdownSkinDesktop, tenantId),
                    DisplaySubmenuArrowDesktop = await _settingManager.GetSettingValueForTenantAsync<bool>(AppSettings.UiManagement.Header.DisplaySubmenuArrowDesktop, tenantId),
                    DropdownSkin = await _settingManager.GetSettingValueForTenantAsync(AppSettings.UiManagement.Header.DropdownSkin, tenantId)
                },
                Menu = new UiCustomizationMenuSettingsEditDto
                {
                    Position = await _settingManager.GetSettingValueForTenantAsync(AppSettings.UiManagement.LeftAside.Position, tenantId),
                    AsideSkin = await _settingManager.GetSettingValueForTenantAsync(AppSettings.UiManagement.LeftAside.AsideSkin, tenantId),
                    FixedAside = await _settingManager.GetSettingValueForTenantAsync<bool>(AppSettings.UiManagement.LeftAside.FixedAside, tenantId),
                    AllowAsideMinimizing = await _settingManager.GetSettingValueForTenantAsync<bool>(AppSettings.UiManagement.LeftAside.AllowAsideMinimizing, tenantId),
                    DefaultMinimizedAside = await _settingManager.GetSettingValueForTenantAsync<bool>(AppSettings.UiManagement.LeftAside.DefaultMinimizedAside, tenantId),
                    AllowAsideHiding = await _settingManager.GetSettingValueForTenantAsync<bool>(AppSettings.UiManagement.LeftAside.AllowAsideHiding, tenantId),
                    DefaultHiddenAside = await _settingManager.GetSettingValueForTenantAsync<bool>(AppSettings.UiManagement.LeftAside.DefaultHiddenAside, tenantId),
                    SubmenuToggle = await _settingManager.GetSettingValueForTenantAsync(AppSettings.UiManagement.LeftAside.SubmenuToggle, tenantId),
                    DropdownSubmenuSkin = await _settingManager.GetSettingValueForTenantAsync(AppSettings.UiManagement.LeftAside.DropdownSubmenuSkin, tenantId),
                    DropdownSubmenuArrow = await _settingManager.GetSettingValueForTenantAsync<bool>(AppSettings.UiManagement.LeftAside.DropdownSubmenuArrow, tenantId)
                },
                Footer = new UiCustomizationFooterSettingsEditDto
                {
                    FixedFooter = await _settingManager.GetSettingValueForTenantAsync<bool>(AppSettings.UiManagement.Footer.FixedFooter, tenantId)
                }
            };
        }

        private async Task UpdateTenantUiManagementSettingsAsync(UiCustomizationSettingsEditDto settings)
        {
            var tenantId = AbpSession.GetTenantId();

            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.LayoutType, settings.Layout.LayoutType);
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.PageLoader, settings.Layout.PageLoader);
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.ContentSkin, settings.Layout.ContentSkin);

            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.Header.DesktopFixedHeader, settings.Header.DesktopFixedHeader.ToString());
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.Header.DesktopMinimizeMode, settings.Header.DesktopMinimizeMode);
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.Header.MobileFixedHeader, settings.Header.MobileFixedHeader.ToString());
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.Header.DropdownSkinDesktop, settings.Header.DropdownSkinDesktop);
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.Header.DisplaySubmenuArrowDesktop, settings.Header.DisplaySubmenuArrowDesktop.ToString());
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.Header.DropdownSkin, settings.Header.DropdownSkin);

            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.LeftAside.Position, settings.Menu.Position);
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.LeftAside.AsideSkin, settings.Menu.AsideSkin);
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.LeftAside.FixedAside, settings.Menu.FixedAside.ToString());
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.LeftAside.AllowAsideMinimizing, settings.Menu.AllowAsideMinimizing.ToString());
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.LeftAside.DefaultMinimizedAside, settings.Menu.DefaultMinimizedAside.ToString());
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.LeftAside.AllowAsideHiding, settings.Menu.AllowAsideHiding.ToString());
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.LeftAside.DefaultHiddenAside, settings.Menu.DefaultHiddenAside.ToString());
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.LeftAside.SubmenuToggle, settings.Menu.SubmenuToggle);
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.LeftAside.DropdownSubmenuSkin, settings.Menu.DropdownSubmenuSkin);
            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.LeftAside.DropdownSubmenuArrow, settings.Menu.DropdownSubmenuArrow.ToString());

            await _settingManager.ChangeSettingForTenantAsync(tenantId, AppSettings.UiManagement.Footer.FixedFooter, settings.Footer.FixedFooter.ToString());
        }

        private async Task UpdateApplicationUiManagementSettingsAsync(UiCustomizationSettingsEditDto settings)
        {
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.LayoutType, settings.Layout.LayoutType);
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.PageLoader, settings.Layout.PageLoader);
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.ContentSkin, settings.Layout.ContentSkin);

            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.Header.DesktopFixedHeader, settings.Header.DesktopFixedHeader.ToString());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.Header.DesktopMinimizeMode, settings.Header.DesktopMinimizeMode);
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.Header.MobileFixedHeader, settings.Header.MobileFixedHeader.ToString());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.Header.DropdownSkinDesktop, settings.Header.DropdownSkinDesktop);
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.Header.DisplaySubmenuArrowDesktop, settings.Header.DisplaySubmenuArrowDesktop.ToString());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.Header.DropdownSkin, settings.Header.DropdownSkin);

            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.LeftAside.Position, settings.Menu.Position);
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.LeftAside.AsideSkin, settings.Menu.AsideSkin);
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.LeftAside.FixedAside, settings.Menu.FixedAside.ToString());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.LeftAside.AllowAsideMinimizing, settings.Menu.AllowAsideMinimizing.ToString());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.LeftAside.DefaultMinimizedAside, settings.Menu.DefaultMinimizedAside.ToString());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.LeftAside.AllowAsideHiding, settings.Menu.AllowAsideHiding.ToString());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.LeftAside.DefaultHiddenAside, settings.Menu.DefaultHiddenAside.ToString());
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.LeftAside.SubmenuToggle, settings.Menu.SubmenuToggle);
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.LeftAside.DropdownSubmenuSkin, settings.Menu.DropdownSubmenuSkin);
            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.LeftAside.DropdownSubmenuArrow, settings.Menu.DropdownSubmenuArrow.ToString());

            await _settingManager.ChangeSettingForApplicationAsync(AppSettings.UiManagement.Footer.FixedFooter, settings.Footer.FixedFooter.ToString());
        }

        private async Task UpdateUserUiManagementSettingsAsync(UiCustomizationSettingsEditDto settings)
        {
            var userIdentifier = AbpSession.ToUserIdentifier();

            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.LayoutType, settings.Layout.LayoutType);
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.PageLoader, settings.Layout.PageLoader);
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.ContentSkin, settings.Layout.ContentSkin);

            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.Header.DesktopFixedHeader, settings.Header.DesktopFixedHeader.ToString());
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.Header.DesktopMinimizeMode, settings.Header.DesktopMinimizeMode);
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.Header.MobileFixedHeader, settings.Header.MobileFixedHeader.ToString());
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.Header.DropdownSkinDesktop, settings.Header.DropdownSkinDesktop);
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.Header.DisplaySubmenuArrowDesktop, settings.Header.DisplaySubmenuArrowDesktop.ToString());
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.Header.DropdownSkin, settings.Header.DropdownSkin);

            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.LeftAside.Position, settings.Menu.Position);
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.LeftAside.AsideSkin, settings.Menu.AsideSkin);
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.LeftAside.FixedAside, settings.Menu.FixedAside.ToString());
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.LeftAside.AllowAsideMinimizing, settings.Menu.AllowAsideMinimizing.ToString());
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.LeftAside.DefaultMinimizedAside, settings.Menu.DefaultMinimizedAside.ToString());
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.LeftAside.AllowAsideHiding, settings.Menu.AllowAsideHiding.ToString());
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.LeftAside.DefaultHiddenAside, settings.Menu.DefaultHiddenAside.ToString());
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.LeftAside.SubmenuToggle, settings.Menu.SubmenuToggle);
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.LeftAside.DropdownSubmenuSkin, settings.Menu.DropdownSubmenuSkin);
            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.LeftAside.DropdownSubmenuArrow, settings.Menu.DropdownSubmenuArrow.ToString());

            await _settingManager.ChangeSettingForUserAsync(userIdentifier, AppSettings.UiManagement.Footer.FixedFooter, settings.Footer.FixedFooter.ToString());
        }
    }
}
