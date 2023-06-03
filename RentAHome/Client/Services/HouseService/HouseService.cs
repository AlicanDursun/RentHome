namespace RentAHome.Client.Services.HouseServices
{
    public class HouseService : IHouseService
    {
        private readonly HttpClient _http;

        public event Action HouseListChanged;

        public HouseService(HttpClient http)
        {
            _http = http;
        }

        public List<House> Houses { get; set; } = new List<House>();
        public List<House> UserHouses { get; set; } = new List<House>();

        public async Task GetRangeHouseList(int startIndex, int count, bool firstRender, string category)
        {
            if (category == "")
            {
               
                var result = await _http.GetFromJsonAsync<ServiceResponse<List<House>>>($"api/House/{startIndex}/{count}");
                if (firstRender)
                    Houses.Clear();
                Houses.AddRange(result.Data);
                HouseListChanged.Invoke();
            }
            else
            {
                var result = await _http.GetFromJsonAsync<ServiceResponse<List<House>>>($"api/House/{startIndex}/{count}");
                if (firstRender)
                    Houses.Clear();

                Houses.AddRange(result.Data);
                HouseListChanged.Invoke();
                
            }


        }

        public async Task<ServiceResponse<House>> GetHouseDetail(int houseId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<House>>($"api/House/{houseId}");
            return result;
        }

        public async Task GetUserHouses()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<House>>>("/api/House/UserHouses");
            UserHouses = result.Data;
        }

        public async Task<ServiceResponse<int>> CreateHouse(House house)
        {
            var result = await _http.PostAsJsonAsync("api/House", house);
            return  await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            
        }

        public async Task<ServiceResponse<int>> UpdateHouse(House house)
        {
            var result = await _http.PutAsJsonAsync("api/House", house);
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<int>>());
        }
        public async Task<bool> HouseFileUpload(MultipartFormDataContent files, int houseId)
        {
            var result = await _http.PostAsync($"api/House/HouseImagesUpload/{houseId}", files);
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>()).Data;
        }
        public async Task<bool> DeleteHouse(int houseId)
        {
            var result = await _http.DeleteAsync($"api/House/{houseId}");
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>()).Data;
        }

        
    }
}
