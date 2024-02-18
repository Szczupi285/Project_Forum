using Microsoft.AspNetCore.Identity;
using Project_Forum.Models.Entities;
using Project_Forum.Services.Login;
using Project_Forum.Models;
using Xunit;
// namespace alias for ambigous reference with LoginModel and
// LoginModel from Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal 
using Mod = Project_Forum.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Forum_xUnitTests
{
    public class LoginServiceUnitTest
    {
        [Fact]
        public async Task ValidateCreditentials_ShouldReturnTrue_WhenCreditentialsAreValid()
        {
            // Arrange
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            userManagerMock.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<ApplicationUser>>().Object);

            var loginService = new LoginService(userManagerMock.Object, signInManagerMock.Object);

            var loginModel = new Mod.LoginModel
            {
                Username = "ValidPassword",
                Password = "InvalidPassword"
            };

            var user = new ApplicationUser
            {
                UserName = loginModel.Username,
            };

            userManagerMock.Setup(x => x.FindByNameAsync(loginModel.Username)).ReturnsAsync(user);
            signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, loginModel.Password, false))
                            .ReturnsAsync(SignInResult.Success);

            // Act
            bool result = await loginService.ValidateCreditentials(loginModel);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task ValidateCreditentials_ShouldReturnFalse_WhenCreditentialsAreInvalid()
        {
            // Arrange
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            userManagerMock.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<ApplicationUser>>().Object);

            var loginService = new LoginService(userManagerMock.Object, signInManagerMock.Object);

            var loginModel = new Mod.LoginModel
            {
                Username = "InvalidLogin",
                Password = "InvalidPassword"
            };

            var user = new ApplicationUser
            {
                UserName = loginModel.Username,
            };

            userManagerMock.Setup(x => x.FindByNameAsync(loginModel.Username)).ReturnsAsync(user);
            signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, loginModel.Password, false))
                            .ReturnsAsync(SignInResult.Failed);

            // Act
            bool result = await loginService.ValidateCreditentials(loginModel);

            // Assert
            Assert.False(result);
        }

    }
}