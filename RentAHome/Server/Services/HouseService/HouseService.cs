

namespace RentAHome.Server.Services.HouseService
{
    public class HouseService : IHouseService
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAuthService _authService;

        public HouseService(DataContext context, IWebHostEnvironment env, IAuthService authService)
        {
            _context = context;
            _env = env;
            _authService = authService;
        }
        public async Task<ServiceResponse<List<House>>> GetRangeHouseList(int startIndex, int count)
        {
            var userId = _authService.GetCurrentUserId();
            var response = new ServiceResponse<List<House>>();
            if (userId > 0)
            {
                response.Data = response.Data = await _context.Houses
                .Include(w => w.HouseAddress)
                .ThenInclude(w => w.Country)
                .Include(w => w.HouseAddress)
                .ThenInclude(w => w.City)
                .Include(w => w.HouseFeature)
                .Include(w => w.Images)
                .Include(w => w.Favorites.Where(w => w.UserId == userId && w.HouseId == w.House.Id))
                .OrderBy(w => w.Id)
                .Skip(startIndex)
                .Take(count)
                .ToListAsync();
            }
            else
            {

                response.Data = await _context.Houses
            .Include(w => w.HouseAddress)
            .ThenInclude(w => w.Country)
            .Include(w => w.HouseAddress)
            .ThenInclude(w => w.City)
            .Include(w => w.HouseFeature)
            .Include(w => w.Images)
            .OrderBy(w => w.Id)
            .Skip(startIndex)
            .Take(count)
            .ToListAsync();
            }
            return response;
        }
        public async Task<ServiceResponse<List<House>>> UserAddedHousesList()
        {
            int userId = _authService.GetCurrentUserId();

            var response = new ServiceResponse<List<House>>
            {
                Data = await _context.Houses
                .Include(w => w.HouseFeature)
                .Where(w => w.UserId == userId)
                 .ToListAsync()
            };
            return response;
        }
        public async Task<ServiceResponse<House>> GetHouseDetail(int houseId)
        {
            var response = new ServiceResponse<House>();

            House house = await _context.Houses.Where(w => w.Id == houseId)
               .Include(w => w.HouseAddress)
               .ThenInclude(w => w.Country)
               .Include(w => w.HouseAddress)
               .ThenInclude(w => w.City)
               .Include(w => w.HouseFeature)
               .Include(w => w.Images)
               .FirstOrDefaultAsync();
            if (house == null)
            {
                response.Success = false;
                response.Message = "Sorry, The house you were looking at was not found";
            }
            else
            {
                response.Data = house;
            }
            return response;
        }
        public async Task<ServiceResponse<int>> CreateHouse(House house)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();

            house.HouseAddress.CityId = house.HouseAddress.City.Id;
            house.HouseAddress.CountryId = house.HouseAddress.Country.Id;
            house.HouseAddress.City = null;
            house.HouseAddress.Country = null;
            var userId = _authService.GetCurrentUserId();
            house.UserId = userId;
            try
            {
                _context.Add(house);
                await _context.SaveChangesAsync();
                response.Data = house.Id;


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            response.Data = house.Id;
            return response;
        }
        public async Task<ServiceResponse<int>> UpdateHouse(House house)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            try
            {
                var dbHouse = await _context.Houses
                    .Where(w => w.Id == house.Id)
                    .Include(w => w.HouseFeature)
                    .Include(w => w.HouseAddress)
                    .Include(w=>w.Images)
                    .FirstOrDefaultAsync();

                if (dbHouse == null)
                {
                    response.Success = false;
                    response.Message = "House not found";
                }
                else
                {
                    dbHouse.HouseTitle = house.HouseTitle;
                    dbHouse.HouseDescription = house.HouseDescription;
                    dbHouse.HouseAddress = house.HouseAddress;
                    dbHouse.HouseFeature = house.HouseFeature;
                    dbHouse.Images = house.Images;
                    dbHouse.HouseAddress.CityId = house.HouseAddress.City.Id;
                    dbHouse.HouseAddress.CountryId = house.HouseAddress.Country.Id;
                    dbHouse.HouseAddress.City = null;
                    dbHouse.HouseAddress.Country = null;
                    await _context.SaveChangesAsync();
                    response.Data = house.Id;
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<ServiceResponse<bool>> HouseImagesUpload(IEnumerable<IFormFile> files, int houseId)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();

            if (files.Count() > 0)
            {
                foreach (var item in files)
                {
                    
                    var path = Path.Combine(_env.ContentRootPath.Replace("Server", "Client"),
                            "wwwroot", "images", Convert.ToString(houseId)
                            );

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    try
                    {
                        path = Path.Combine(path,item.FileName);
                        await using FileStream fs = new(path, FileMode.Create);
                        await item.CopyToAsync(fs);
                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        response.Message = ex.Message.ToString();
                    }

                }
            }
            return response;
        }

        //public async Task<ServiceResponse<bool>> DeleteHouse(int houseId)
        //{
        //    try
        //    {
        //        var dbHouse = await _context.Houses
        //            .Include(w => w.HouseAddress).Include(w => w.HouseFeature)
        //            .FirstOrDefaultAsync(w => w.Id == houseId);
        //        if (dbHouse == null)
        //        {
        //            return new ServiceResponse<bool>
        //            {
        //                Success = false,
        //                Data = false,
        //                Message = "House not found",
        //            };
        //        }
        //        _context.Remove(dbHouse);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        var hayvan = ex.Message.ToString();
        //    }

        //    return new ServiceResponse<bool> { Success = true, Data = true };

        //}


    }
}
