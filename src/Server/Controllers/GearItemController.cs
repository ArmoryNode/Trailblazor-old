using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trailblazor.Shared.Services;
using Trailblazor.Shared.ViewModels;

using static Trailblazor.Constants.Authorization;

namespace Trailblazor.Server.Controllers
{
    //[Authorize]
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
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, gearItem, Policies.GearItemOwner);

                if (authorizationResult.Succeeded)
                    userGearItems.Add(gearItem);
            }

            return Ok(userGearItems);
        }

        [HttpGet("/{gearItemId}")]
        public async ValueTask<IActionResult> GetGearItem(string gearItemId, CancellationToken cancellationToken)
        {
            var gearItem = await _gearItemService.GetById(gearItemId, cancellationToken);
            if (gearItem == null)
                return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, gearItem, Policies.GearItemOwner);

            if (authorizationResult.Succeeded)
                return Ok(gearItem);

            return NotFound();
        }
    }
}
