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
    public class LoginServiceUnitTests
    {
        [Fact]
        public async Task ValidateCreditentials_ShouldReturnTrue_WhenCreditentialsAreValid()
        {
            MockedIdentity mockedIdentity = new MockedIdentity();

            var loginService = new LoginService(mockedIdentity.userManagerMock.Object, mockedIdentity.signInManagerMock.Object);

            var loginModel = new Mod.LoginModel
            {
                Username = "ValidPassword",
                Password = "ValidPassword"
            };

            var user = new ApplicationUser
            {
                UserName = loginModel.Username,
            };

            mockedIdentity.userManagerMock.Setup(x => x.FindByNameAsync(loginModel.Username)).ReturnsAsync(user);
            mockedIdentity.signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, loginModel.Password, false))
                            .ReturnsAsync(SignInResult.Success);

            // Act
            bool result = await loginService.ValidateCreditentials(loginModel);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task ValidateCreditentials_ShouldReturnFalse_WhenCreditentialsAreInvalid()
        {
            MockedIdentity mockedIdentity = new MockedIdentity();

            var loginService = new LoginService(mockedIdentity.userManagerMock.Object, mockedIdentity.signInManagerMock.Object);

            var loginModel = new Mod.LoginModel
            {
                Username = "InvalidLogin",
                Password = "InvalidPassword"
            };

            var user = new ApplicationUser
            {
                UserName = loginModel.Username,
            };

            mockedIdentity.userManagerMock.Setup(x => x.FindByNameAsync(loginModel.Username)).ReturnsAsync(user);
            mockedIdentity.signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, loginModel.Password, false))
                            .ReturnsAsync(SignInResult.Failed);

            // Act
            bool result = await loginService.ValidateCreditentials(loginModel);

            // Assert
            Assert.False(result);
        }

    }
}