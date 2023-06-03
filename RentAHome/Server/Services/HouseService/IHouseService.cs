namespace RentAHome.Server.Services.HouseService
{
    public interface IHouseService
    {
       
        Task<ServiceResponse<List<House>>> GetRangeHouseList(int startIndex, int count);
        Task<ServiceResponse<House>> GetHouseDetail(int houseId);
        Task<ServiceResponse<List<House>>> UserAddedHousesList();

        Task<ServiceResponse<int>> CreateHouse(House house);
        Task<ServiceResponse<int>> UpdateHouse(House house);

        //Task<ServiceResponse<bool>> DeleteHouse(int houseId);

        Task<ServiceResponse<bool>> HouseImagesUpload(IEnumerable<IFormFile> files, int houseId);
    }
}
