﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trailblazor.Server.Handlers.Authorization;
using Trailblazor.Server.Models.Data;
using Trailblazor.Server.Services;
using Trailblazor.Shared.Services;
using Trailblazor.Shared.ViewModels;

using static Trailblazor.Constants.Authorization;

namespace Trailblazor.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GearItemController : ControllerBase
    {
        private readonly IDataService<GearItemViewModel> _gearItemService;
        private readonly IAuthorizationService _authorizationService;

        public GearItemController(IDataService<GearItemViewModel> gearItemService, IAuthorizationService authorizationService)
        {
            _gearItemService = gearItemService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllGearItems(CancellationToken cancellationToken)
        {
            var gearItems = await _gearItemService.GetAll(cancellationToken);

            var userGearItems = new List<GearItemViewModel>();

            foreach (var gearItem in gearItems)
            {
                var result = await _authorizationService.AuthorizeAsync(User, gearItem, Policies.GearItemOwner);

                if (result.Succeeded)
                    userGearItems.Add(gearItem);
            }

            return Ok(userGearItems);
        }
    }
}
