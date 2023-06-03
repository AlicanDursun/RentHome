namespace RentAHome.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> RegisterOrLogin(User user);

        Task<bool> IsUserAuthenticated();

       
    }
}
