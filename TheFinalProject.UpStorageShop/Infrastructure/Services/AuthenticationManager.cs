using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Models.Auth;
using Domain.Identity;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;


        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<Guid> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var user = createUserDto.MapToUser();

            var identityResult = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!identityResult.Succeeded)
            {
                var failures = identityResult.Errors
                    .Select(x => new ValidationFailure(x.Code, x.Description));

                throw new ValidationException(failures);
            }

            return user.Id;

        }

        public async Task<string> GenerateEmailActivationTokenAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);

        }

        public Task<bool> CheckIfUserExists(string email, CancellationToken cancellationToken)
        {
            return _userManager.Users.AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<JwtDto> LoginAsync(AuthLoginRequest authLoginRequest, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(authLoginRequest.Email);

            var loginResult = await _signInManager.PasswordSignInAsync(user, authLoginRequest.Password, false, false);

            if (!loginResult.Succeeded)
            {
                throw new ValidationException(CreateValidationFailure);
            }

            return _jwtService.Generate(user.Id, user.Email, user.FirstName, user.LastName, null, user.IsMailAllowed, user.IsNotificationAllowed);
        }

        public async Task<JwtDto> SocialLoginAsync(string email, string firstName, string lastName, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
                return _jwtService.Generate(user.Id, user.Email, user.FirstName, user.LastName, null, user.IsMailAllowed, user.IsNotificationAllowed);

            var userId = Guid.NewGuid();

            user = new User()
            {
                Id = userId,
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                FirstName = firstName,
                LastName = lastName,
                CreatedOn = DateTimeOffset.Now,
                CreatedByUserId = userId.ToString(),
            };

            var identityResult = await _userManager.CreateAsync(user);

            if (!identityResult.Succeeded)
            {
                var failures = identityResult.Errors
                    .Select(x => new ValidationFailure(x.Code, x.Description));

                throw new ValidationException(failures);
            }

            return _jwtService.Generate(user.Id, user.Email, user.FirstName, user.LastName, null, user.IsMailAllowed, user.IsNotificationAllowed);
        }

        private List<ValidationFailure> CreateValidationFailure => new List<ValidationFailure>()
        {
            new ValidationFailure("Email & Password","Email or password incorrect.")
        };
    }
}
