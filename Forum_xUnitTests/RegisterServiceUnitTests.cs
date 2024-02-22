using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Project_Forum.Models;
using Project_Forum.Models.Entities;
using Project_Forum.Services.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_xUnitTests
{
    public class RegisterServiceUnitTests
    {
        [Fact]
        public async Task RegisterUser_ShouldReturnFalse_WhenBothUsernameAndEmailAlreadyExists()
        {
            // Arrange
            MockedIdentity mockedIdentity = new MockedIdentity();
            ModelStateDictionary mStateDict = new ModelStateDictionary();

            var registerService = new RegisterService();

            var existingUser = new ApplicationUser
            {
                UserName = "ExistingUsername",
                Email = "ExistingEmail@gmail.com",
            };

            var registerModel = new RegisterModel
            {
                Username = "ExistingUsername",
                Password = "Password123!",
                Email = "ExistingEmail@gmail.com",
                Date = DateTime.Parse("1997.10.10")

            };

            mockedIdentity.userManagerMock.Setup(m => m.FindByEmailAsync(registerModel.Email)).ReturnsAsync(existingUser);
            mockedIdentity.userManagerMock.Setup(m => m.FindByNameAsync(registerModel.Username)).ReturnsAsync(existingUser);

            // Act
            var result = await registerService.RegisterUser(mockedIdentity.userManagerMock.Object, registerModel, mStateDict);

            // Assert 
            Assert.False(result);
        }

        [Fact]
        public async Task RegisterUser_ShouldContainError_WhenModelErrorForEmailIsAddedCorrectly_WhenBothUsernameAndPasswordAlreadyExist()
        {
            // Arrange
            MockedIdentity mockedIdentity = new MockedIdentity();
            ModelStateDictionary mStateDict = new ModelStateDictionary();

            var registerService = new RegisterService();

            var existingUser = new ApplicationUser
            {
                UserName = "ExistingUsername",
                Email = "ExistingEmail@gmail.com",
            };

            var registerModel = new RegisterModel
            {
                Username = "ExistingUsername",
                Password = "Password123!",
                Email = "ExistingEmail@gmail.com",
                Date = DateTime.Parse("1997.10.10")

            };

            mockedIdentity.userManagerMock.Setup(m => m.FindByEmailAsync(registerModel.Email)).ReturnsAsync(existingUser);
            mockedIdentity.userManagerMock.Setup(m => m.FindByNameAsync(registerModel.Username)).ReturnsAsync(existingUser);

            // Act
            var result = await registerService.RegisterUser(mockedIdentity.userManagerMock.Object, registerModel, mStateDict);

            // Assert
            Assert.Contains(mStateDict["Email"]!.Errors[0].ErrorMessage, "Email already taken");
        }

        [Fact]
        public async Task RegisterUser_ShouldContainError_WhenModelErrorForUsernameIsAddedCorrectly_WhenBothUsernameAndPasswordAlreadyExist()
        {
            // Arrange
            MockedIdentity mockedIdentity = new MockedIdentity();
            ModelStateDictionary mStateDict = new ModelStateDictionary();

            var registerService = new RegisterService();

            var existingUser = new ApplicationUser
            {
                UserName = "ExistingUsername",
                Email = "ExistingEmail@gmail.com",
            };

            var registerModel = new RegisterModel
            {
                Username = "ExistingUsername",
                Password = "Password123!",
                Email = "ExistingEmail@gmail.com",
                Date = DateTime.Parse("1997.10.10")

            };

            mockedIdentity.userManagerMock.Setup(m => m.FindByEmailAsync(registerModel.Email)).ReturnsAsync(existingUser);
            mockedIdentity.userManagerMock.Setup(m => m.FindByNameAsync(registerModel.Username)).ReturnsAsync(existingUser);

            // Act
            var result = await registerService.RegisterUser(mockedIdentity.userManagerMock.Object, registerModel, mStateDict);
            // Assert
            Assert.Contains(mStateDict["Username"]!.Errors[0].ErrorMessage, "Username already taken");
        }

        [Fact]
        public async Task RegisterUser_ShouldContainErrorForEmail_WhenEmailAlreadyExists()
        {
            // Arrange
            MockedIdentity mockedIdentity = new MockedIdentity();
            ModelStateDictionary mStateDict = new ModelStateDictionary();

            var registerService = new RegisterService();

            var existingUser = new ApplicationUser
            {
                UserName = "ExistingUsername",
                Email = "ExistingEmail@gmail.com",
            };

            var registerModel = new RegisterModel
            {
                Username = "ExistingUsername",
                Password = "Password123!",
                Email = "ExistingEmail@gmail.com",
                Date = DateTime.Parse("1997.10.10")

            };

            mockedIdentity.userManagerMock.Setup(m => m.FindByEmailAsync(registerModel.Email)).ReturnsAsync(existingUser);
            mockedIdentity.userManagerMock.Setup(m => m.FindByNameAsync(registerModel.Username)).ReturnsAsync((string user) => null);

            // Act
            var result = await registerService.RegisterUser(mockedIdentity.userManagerMock.Object, registerModel, mStateDict);
            // Assert
            Assert.Contains(mStateDict["Email"]!.Errors[0].ErrorMessage, "E-Mail already taken");
        }

        [Fact]
        public async Task RegisterUser_ShouldContainErrorForUsername_WhenUsernameAlreadyExists()
        {
            // Arrange
            MockedIdentity mockedIdentity = new MockedIdentity();
            ModelStateDictionary mStateDict = new ModelStateDictionary();

            var registerService = new RegisterService();

            var existingUser = new ApplicationUser
            {
                UserName = "ExistingUsername",
                Email = "ExistingEmail@gmail.com",
            };

            var registerModel = new RegisterModel
            {
                Username = "ExistingUsername",
                Password = "Password123!",
                Email = "ExistingEmail@gmail.com",
                Date = DateTime.Parse("1997.10.10")

            };

            mockedIdentity.userManagerMock.Setup(m => m.FindByNameAsync(registerModel.Username)).ReturnsAsync(existingUser);
            mockedIdentity.userManagerMock.Setup(m => m.FindByEmailAsync(registerModel.Email)).ReturnsAsync((string user) => null);

            // Act
            var result = await registerService.RegisterUser(mockedIdentity.userManagerMock.Object, registerModel, mStateDict);
            // Assert
            Assert.Contains(mStateDict["Username"]!.Errors[0].ErrorMessage, "Username already taken");
        }
        [Fact]
        public async Task RegisterUser_ShouldReturnTrue_WhenUsernameAndEmailWasntAlreadyTaken_AndErrorCountIs0()
        {
            // Arrange
            MockedIdentity mockedIdentity = new MockedIdentity();
            ModelStateDictionary mStateDict = new ModelStateDictionary();

            var registerService = new RegisterService();

            var registerModel = new RegisterModel
            {
                Username = "ExistingUsername",
                Password = "Password123!",
                Email = "ExistingEmail@gmail.com",
                Date = DateTime.Parse("1997.10.10")

            };

            mockedIdentity.userManagerMock.Setup(m => m.FindByNameAsync(registerModel.Username)).ReturnsAsync((string user) => null);
            mockedIdentity.userManagerMock.Setup(m => m.FindByEmailAsync(registerModel.Email)).ReturnsAsync((string user) => null);

            ApplicationUser user = new ApplicationUser
            {
                UserName = registerModel.Username,
                Email = registerModel.Email,
                DateOfBirth = registerModel.Date,
                LockoutEnabled = false,

            };

            mockedIdentity.userManagerMock
            .Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

            mockedIdentity.userManagerMock
                 .Setup(m => m.FindByNameAsync(user.UserName))
                 .ReturnsAsync(It.IsAny<ApplicationUser>());

            mockedIdentity.userManagerMock
                .Setup(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await registerService.RegisterUser(mockedIdentity.userManagerMock.Object, registerModel, mStateDict);
            // Assert
            Assert.True(mStateDict.ErrorCount == 0);
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnTrue_WhenUsernameAndEmailWasntAlreadyTaken_AndRegistrationWentSuccesfull()
        {
            // Arrange
            MockedIdentity mockedIdentity = new MockedIdentity();
            ModelStateDictionary mStateDict = new ModelStateDictionary();

            var registerService = new RegisterService();

            var registerModel = new RegisterModel
            {
                Username = "ExistingUsername",
                Password = "Password123!",
                Email = "ExistingEmail@gmail.com",
                Date = DateTime.Parse("1997.10.10")

            };

            mockedIdentity.userManagerMock.Setup(m => m.FindByNameAsync(registerModel.Username)).ReturnsAsync((string user) => null);
            mockedIdentity.userManagerMock.Setup(m => m.FindByEmailAsync(registerModel.Email)).ReturnsAsync((string user) => null);

            ApplicationUser user = new ApplicationUser
            {
                UserName = registerModel.Username,
                Email = registerModel.Email,
                DateOfBirth = registerModel.Date,
                LockoutEnabled = false,

            };
           
            mockedIdentity.userManagerMock
             .Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
             .ReturnsAsync(IdentityResult.Success);

            mockedIdentity.userManagerMock
                 .Setup(m => m.FindByNameAsync(user.UserName))
                 .ReturnsAsync(It.IsAny<ApplicationUser>());

            mockedIdentity.userManagerMock
                .Setup(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await registerService.RegisterUser(mockedIdentity.userManagerMock.Object, registerModel, mStateDict);
            // Assert
            Assert.True(result);


        }
    }
}
