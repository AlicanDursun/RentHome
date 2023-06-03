namespace RentAHome.Server.Services.FavoriteService
{
    public class FavoriteService : IFavoriteService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public FavoriteService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        public async Task<ServiceResponse<List<Favorite>>> GetUserFavorites()
        {
            var userId = _authService.GetCurrentUserId();
            return new ServiceResponse<List<Favorite>>
            {
                Data = await _context.Favorites.Where(x => x.UserId == userId).ToListAsync(),
            };
        }
        public async Task<ServiceResponse<bool>> AddFavorite(int houseId)
        {

            var dbFavorite = await _context.Favorites.Where(w => w.HouseId == houseId && w.UserId == _authService.GetCurrentUserId()).FirstOrDefaultAsync();
            if (dbFavorite == null)
                _context.Add(new Favorite { HouseId = houseId, UserId = _authService.GetCurrentUserId() });
            else
                _context.Favorites.Remove(dbFavorite);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true, Success = true };
        }

        //public async Task<ServiceResponse<List<FavoriteHouseResponse>>> GetFavoriteHouses(List<Favorite> favorites)
        //{
        //    var result = new ServiceResponse<List<FavoriteHouseResponse>>
        //    {
        //        Data = new List<FavoriteHouseResponse>()
        //    };
        //    foreach (var item in favorites)
        //    {
        //        var house = await _context.Houses
        //            .Where(w => w.Id == item.HouseId)
        //            .Include(w => w.HouseFeature)
        //            .Include(w => w.HouseAddress)
        //            .Include(w => w!.Country)
        //            .Include(w => w!.City)
        //            .FirstOrDefaultAsync();
        //        if (house == null) continue;
        //        var favoriteHouse = new FavoriteHouseResponse
        //        {
        //            HouseId = house.Id,
        //            HouseCountry = house.Country!.Name,
        //            HouseCity = house.City!.Name,
        //            HouseStreet = house.HouseAddress.Street,
        //            ImageUrl = "images/pilars.jpg",
        //            Price = house.HouseFeature!.Price
        //        };
        //        result.Data.Add(favoriteHouse);
        //    }
        //    return result;
        //}

      


    }
}
