namespace RentAHome.Server.Services.HouseInfoService
{
    public class HouseInfoService : IHouseInfoService
    {
        private readonly DataContext _context;

        public HouseInfoService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<City>>> GetCityList(int countryId)
        {
            return new ServiceResponse<List<City>>
            {
                Data = await _context.Cities.Where(w => w.CountryId == countryId).ToListAsync()

            };
        }

        public async Task<ServiceResponse<List<Country>>> GetCountryList()
        {

            return new ServiceResponse<List<Country>>
            {
                Data = await _context.Countries.ToListAsync()

            };
        }

        //public async Task<ServiceResponse<List<HouseType>>> GetHouseTypeList()
        //{
        //    return new ServiceResponse<List<HouseType>>
        //    {
        //        Data = await _context.HouseTypes.ToListAsync()

        //    };

        //}
    }
}
