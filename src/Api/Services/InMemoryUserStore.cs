using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Api.Services
{
    public class InMemoryUserStore : IUserEmailStore<IdentityUser>
    {
        private readonly IList<IdentityUser> users;
        public InMemoryUserStore()
        {
            users = new List<IdentityUser>();
        }

        public async Task<IdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var userOrNull = users.SingleOrDefault(user => user.NormalizedEmail == normalizedEmail);
            return await Task.FromResult(userOrNull);
        }

        public async Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Email);
        }

        public async Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var userOrNull = users.SingleOrDefault(user => user.NormalizedUserName == normalizedUserName);
            return await Task.FromResult(userOrNull);
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            users.Add(user);
            return await Task.FromResult(IdentityResult.Success);
        }

        public Task SetNormalizedEmailAsync(IdentityUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }

        public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Email);
        }

        public Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetEmailAsync(IdentityUser user, string email, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}