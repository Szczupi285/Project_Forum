using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Project_Forum.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_xUnitTests
{
    public class MockedIdentity
    {
        public Mock<UserManager<ApplicationUser>> userManagerMock;

        public Mock<SignInManager<ApplicationUser>> signInManagerMock;
        
        public MockedIdentity()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            userManagerMock.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            userManagerMock.Object,
            new HttpContextAccessor(),
            new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<ApplicationUser>>().Object);
        }
    }
}
