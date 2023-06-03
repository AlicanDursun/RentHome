namespace RentAHome.Client.Services.HouseInfoService
{
    public interface IHouseInfoService
    {
        event Action OnChange;
        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; } 
        public List<HouseType> HouseTypes { get; set; }

        Task GetCountries();
        Task GetCities(Country country);
        Task GetHouseTypes();

    }
}
