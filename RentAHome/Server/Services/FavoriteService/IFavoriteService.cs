namespace RentAHome.Server.Services.FavoriteService
{
    public interface IFavoriteService
    {


        Task<ServiceResponse<bool>> AddFavorite(int houseId);
        Task<ServiceResponse<List<Favorite>>> GetUserFavorites();
        //Task<ServiceResponse<List<FavoriteHouseResponse>>> GetFavoriteHouses(List<Favorite> favorites);
    }
}
