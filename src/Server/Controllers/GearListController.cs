using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trailblazor.Server.Services;
using Trailblazor.Shared;
using Trailblazor.Shared.Services;
using Trailblazor.Shared.ViewModels;
using static Trailblazor.Constants.Authorization;

namespace Trailblazor.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class GearListController : ControllerBase
    {
        private readonly IDataService<GearListViewModel> _gearListService;
        private readonly IAuthorizationService _authorizationService;

        public GearListController(IDataService<GearListViewModel> gearListService, IAuthorizationService authorizationService)
        {
            _gearListService = gearListService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllGearLists(CancellationToken cancellationToken)
        {
            var userGearList = new List<GearListViewModel>();

            var queryOptions = new DataServiceQueryOptions { OwnerId = User.GetUserId<Guid>() };

            foreach (var gearItem in await _gearListService.GetAllPartial(queryOptions, cancellationToken))
            {
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, gearItem, Policies.GetListOwner);

                if (authorizationResult.Succeeded)
                    userGearList.Add(gearItem);
            }

            return Ok(userGearList);
        }

        [HttpGet("{gearListId}")]
        public async ValueTask<IActionResult> GetGearList(Guid gearListId, CancellationToken cancellationToken)
        {
            var gearItem = await _gearListService.GetById(gearListId, cancellationToken);

            if (gearItem == null)
                return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, gearItem, Policies.GetListOwner);

            if (authorizationResult.Succeeded)
                return Ok(gearItem);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> InsertGearList(GearListViewModel viewModel, CancellationToken cancellationToken)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, viewModel, Policies.GetListOwner);

            if (authorizationResult.Succeeded)
                await _gearListService.Insert(viewModel, cancellationToken);

            return Ok();
        }
    }
}
