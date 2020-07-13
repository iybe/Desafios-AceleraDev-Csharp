using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Codenation.Challenge.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;

namespace Codenation.Challenge.Services
{
    public class UserProfileService : IProfileService
    {
        CodenationContext dbContext;

        public UserProfileService(CodenationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var request = context.ValidatedRequest as ValidatedTokenRequest;
            if (request != null)
            {
                User user = dbContext.Users.FirstOrDefault(userDb => userDb.Email == request.UserName);
                if (user != null)
                {
                    context.AddRequestedClaims(GetUserClaims(user));
                }
            }

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }

        public static Claim[] GetUserClaims(User user)
        {

            string role = "User";
            if (user.Email == "tegglestone9@blog.com")
                role = "Admin";


            return new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, role)
            };
        }

    }
}