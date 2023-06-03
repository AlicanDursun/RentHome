namespace RentAHome.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> RegisterOrLogin(User user);
        int GetCurrentUserId();
    }
}
