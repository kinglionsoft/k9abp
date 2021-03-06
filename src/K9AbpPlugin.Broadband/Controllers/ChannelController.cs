﻿using System.Collections.Generic;
using System.Threading.Tasks;
using K9AbpPlugin.Broadband.User;
using Microsoft.AspNetCore.Mvc;

namespace K9AbpPlugin.Broadband.Controllers
{
    public class ChannelController : BroadbandControllerBase
    {
        private readonly IBroadbandAppService _broadbandAppService;

        public ChannelController(IBroadbandAppService broadbandAppService)
        {
            _broadbandAppService = broadbandAppService;
        }

        /// <summary>
        /// 获取渠道下的预警情况
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Warn(int id)
        {
            var model = await _broadbandAppService.GetChannelWarnAsync(id);
            return View(model);
        }
    }
}