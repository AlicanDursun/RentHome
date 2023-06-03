using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentAHome.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HouseInfoController : ControllerBase
    {
        private readonly IHouseInfoService _houseInfoService;

        public HouseInfoController(IHouseInfoService houseInfoService)
        {
            _houseInfoService = houseInfoService;
        }


        [HttpGet("countries")]
        public async Task<ActionResult<ServiceResponse<List<Country>>>> GetCountryList()
        {
            return Ok(await _houseInfoService.GetCountryList());
        }
        [HttpGet("cities/{countryId:int}")]
        public async Task<ActionResult<ServiceResponse<List<City>>>> GetCityList(int countryId)
        {
            return Ok(await _houseInfoService.GetCityList(countryId));
        }
        //[HttpGet("houseTypes")]
        //public async Task<ActionResult<ServiceResponse<List<HouseType>>>> GetHouseTypeList()
        //{
        //    return Ok(await _houseInfoService.GetHouseTypeList());
        //}
    }
}
