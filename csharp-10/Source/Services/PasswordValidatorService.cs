using Codenation.Challenge.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Linq;
using System.Threading.Tasks;
 
namespace Codenation.Challenge.Services
{
    public class PasswordValidatorService: IResourceOwnerPasswordValidator
    {
        private readonly CodenationContext dbContext;
        public PasswordValidatorService(CodenationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            User user = dbContext.Users.FirstOrDefault(userDb => userDb.Email == context.UserName && userDb.Password == context.Password);

            if (user != null)
            {
                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod: "custom",
                    claims: UserProfileService.GetUserClaims(user));
                return Task.CompletedTask;
            }
            else
            {
                context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant, "Invalid username or password");
                return Task.CompletedTask;
            }
        }

    }
}