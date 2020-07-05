using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OnlineEducation.Core.Interfaces;

namespace OnlineEducation.Core.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            var id = _httpContextAccessor.HttpContext.User?.Claims
                ?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return id;
        }

        public string GetCurrentUserEmail()
        {
            var email = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)
                ?.Value;

            return email;
        }
    }
}
