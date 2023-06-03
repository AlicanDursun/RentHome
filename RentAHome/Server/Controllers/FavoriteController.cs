using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentAHome.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

       
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Favorite>>>> GetUserFavorites()
        {
            return Ok(await _favoriteService.GetUserFavorites());

        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> AddFavorite([FromBody]int houseId)
        {
            return Ok(await _favoriteService.AddFavorite(houseId));

        }
        //[HttpPost]
        //public async Task<ActionResult<ServiceResponse<List<FavoriteHouseResponse>>>> GetFavoriteHouses(List<Favorite> favorites)
        //{
        //    var result = await _favoriteService.GetFavoriteHouses(favorites);
        //    return Ok(result);
        //}
    }
}
