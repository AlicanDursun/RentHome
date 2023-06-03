using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentAHome.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;
     
        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
           
        }
        [HttpGet("{startIndex}/{count}")]
        public async Task<ActionResult<ServiceResponse<List<House>>>> GetRangeHouseList(int startIndex, int count)
        {
            return Ok(await _houseService.GetRangeHouseList(startIndex, count));
        }

        [HttpGet("{houseId}"), Authorize(Roles = "Customer")]
        public async Task<ActionResult<ServiceResponse<House>>> GetHouseDetail(int houseId)
        {
            return Ok(await _houseService.GetHouseDetail(houseId));
        }
        [HttpGet("UserHouses"), Authorize(Roles = "Customer")]
        public async Task<ActionResult<ServiceResponse<List<House>>>> UserAddedHousesList()
        {
            ServiceResponse<List<House>> house = await _houseService.UserAddedHousesList();
            return house;
            //return Ok(await _houseService.UserAddedHousesList());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<int>>> CreateHouse(House house)
        {
            var result = await _houseService.CreateHouse(house);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<int>>> UpdateHouse(House house)
        {
            var result = await _houseService.UpdateHouse(house);
            return Ok(result);
        }

        [HttpPost("HouseImagesUpload/{houseId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> HouseImagesUpload(IEnumerable<IFormFile> files, int houseId)
        {
            var result = await _houseService.HouseImagesUpload(files, houseId);
            return Ok(result);
        }
        //[HttpDelete("{houseId}")]
        //public async Task<ActionResult<ServiceResponse<bool>>> DeleteHouse(int houseId)
        //{
        //    var result = await _houseService.DeleteHouse(houseId);
        //    return Ok(result);
        //}

    }


}
