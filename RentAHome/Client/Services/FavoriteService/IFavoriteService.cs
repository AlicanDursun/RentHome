namespace RentAHome.Client.Services.FavoriteServices
{
    public interface IFavoriteService
    {
        event Action OnChange;

     

        List<Favorite> Favorites { get; set; }
        Task GetUserFavorites();
        Task AddToFavorites(int houseId);

       
        //Task<List<FavoriteHouseResponse>> GetFavoriteHouses();

        //Task RemoveHouseFromFavorite(int houseId);
    }
}
