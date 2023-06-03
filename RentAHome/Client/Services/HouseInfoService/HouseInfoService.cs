namespace RentAHome.Client.Services.HouseInfoService
{
    public class HouseInfoService : IHouseInfoService
    {
        private readonly HttpClient _http;

        public HouseInfoService(HttpClient http)
        {
            _http = http;
        }
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<City> Cities { get; set; } = new List<City>();
        public List<HouseType> HouseTypes { get; set; } = new List<HouseType>();

        public event Action OnChange;

        public async Task GetCities(Country country)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<City>>>($"api/HouseInfo/cities/{country.Id}");
            if (result!.Data != null)
            {
                Cities = result.Data;
                if(OnChange != null)
                OnChange.Invoke();
            }

        }

        public async Task GetCountries()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Country>>>($"api/HouseInfo/countries");
            if (result!.Data != null)
                Countries = result.Data;
        }

        public async Task GetHouseTypes()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<HouseType>>>($"api/HouseInfo/houseTypes");
            if (result!.Data != null)
                HouseTypes = result.Data;
        }
    }
}
