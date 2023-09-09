using Dealer.WebAPI.Core.Application.Dtos.Accout;
using Dealer.WebAPI.Core.Application.Enums;
using Dealer.WebAPI.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Dealer.WebAPI.Infrastructure.Identity.Service {
    public class AccountService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request) {
            AuthenticationResponse response = new();
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null) {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, true, lockoutOnFailure: false);
            if (!result.Succeeded) {
                response.HasError = true;
                response.Error = $"Invalid Credentials for {request.Email}";
                return response;
            }

            response.Id = user.Id;
            response.UserName = user.UserName;
            response.Email = user.Email;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task<RegisterResponse> RegisterClientUserAsync(RegisterRequest request) {
            RegisterResponse response = new();

            response.HasError = false;
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);

            if (userWithSameUserName != null) {
                response.HasError = true;
                response.Error = $"Username {request.UserName} is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByNameAsync(request.Email);
            if (userWithSameEmail != null) {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already registered.";
                return response;
            }

            var user = new ApplicationUser {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
            } else {
                response.HasError = true;
                response.Error = $"We could not register the user {request.Email}. Please try again.";
                return response;
            }
            return response;
        }
    }
}
