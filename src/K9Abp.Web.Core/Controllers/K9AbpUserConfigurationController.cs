using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using K9Abp.Web.Core.Configuration;

namespace K9Abp.Web.Core.Controllers
{
    public class K9AbpUserConfigurationController: K9AbpControllerBase
    {
        private readonly K9AbpUserConfigurationBuilder _abpUserConfigurationBuilder;

        public K9AbpUserConfigurationController(K9AbpUserConfigurationBuilder abpUserConfigurationBuilder)
        {
            _abpUserConfigurationBuilder = abpUserConfigurationBuilder;
        }

        public async Task<JsonResult> GetAll()
        {
            var userConfig = await _abpUserConfigurationBuilder.GetAll();
            return Json(userConfig);
        }
    }
}

