
namespace RentAHome.Client.Services.HouseServices
{
    public interface IHouseService
    {
        
        event Action HouseListChanged;
        List<House> Houses { get; set; }
        List<House> UserHouses { get; set; }
        Task GetRangeHouseList(int startIndex,int count,bool firstRender,string category);
      
        Task<ServiceResponse<House>> GetHouseDetail(int houseId);
        Task GetUserHouses();

        Task<ServiceResponse<int>> CreateHouse(House house);
        Task<ServiceResponse<int>> UpdateHouse(House house);
        Task<bool> DeleteHouse(int houseId);

        Task<bool> HouseFileUpload(MultipartFormDataContent files, int houseId);
    }
}
