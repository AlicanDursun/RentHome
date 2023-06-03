using Blazored.LocalStorage;

namespace RentAHome.Client.Services.FavoriteServices
{
    public class FavoriteService : IFavoriteService
    {
        private readonly HttpClient _http;

       
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();

        public event Action OnChange;

        public FavoriteService(HttpClient http)
        {
            _http = http;

        }
        public async Task GetUserFavorites()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Favorite>>>("api/favorite");
            Favorites = result.Data;
            OnChange.Invoke();
        }
        public async Task AddToFavorites(int houseId)
        {
            var result = await _http.PostAsJsonAsync("api/favorite", houseId);
            await GetUserFavorites();
            
        }
        //public async Task<List<FavoriteHouseResponse>> GetFavoriteHouses()
        //{
        //    var favorites = await _localStorage.GetItemAsync<List<Favorite>>("favorite");
        //    if (favorites == null)
        //        return new List<FavoriteHouseResponse>();
        //    var response = await _http.PostAsJsonAsync("api/favorite/", favorites);
        //    var houses = await response.Content.ReadFromJsonAsync<ServiceResponse<List<FavoriteHouseResponse>>>();
        //    return houses.Data;
        //}

        //public async Task RemoveHouseFromFavorite(int houseId)
        //{
        //    var favorites = await _localStorage.GetItemAsync<List<Favorite>>("favorite");
        //    if (favorites == null)
        //    {
        //        return;
        //    }
        //    var favoriteItem = favorites.Find(w => w.HouseId == houseId);
        //    if (favoriteItem != null)
        //    {
        //        favorites.Remove(favoriteItem);
        //        await _localStorage.SetItemAsync("favorite",favorites);

        //    }

        //}
    }
}
