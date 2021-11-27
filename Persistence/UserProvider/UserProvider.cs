using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace ofarz_rest_api.Persistence.UserProvider
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _context;

        public UserProvider(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserId()
        {
            return _context.HttpContext.User.Claims
                       .First(i => i.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
