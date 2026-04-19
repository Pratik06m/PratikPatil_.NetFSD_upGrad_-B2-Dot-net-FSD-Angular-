using Week9_Day1_ContactManagementApi.DTOs;
using Week9_Day1_ContactManagementApi.Models;

namespace Week9_Day1_ContactManagementApi.Services
{
    public interface IJwtTokenService
    {
        AuthResponseDto GenerateToken(UserInfo user);
    }
}
