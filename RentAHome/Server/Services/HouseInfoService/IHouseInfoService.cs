namespace RentAHome.Server.Services.HouseInfoService
{
    public interface IHouseInfoService
    {
        Task<ServiceResponse<List<Country>>> GetCountryList();
        Task<ServiceResponse<List<City>>> GetCityList(int countryId);
        //Task<ServiceResponse<List<HouseType>>> GetHouseTypeList();

    }
}
