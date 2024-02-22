using Azure;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Project_Forum.Models.Entities;
using Project_Forum.Services.Observe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Forum_xUnitTests
{
    public class ObserveServiceUnitTest
    {
        [Fact]
        public async void HandleTagObservation_ShouldReturnFalse_WhenUserIsNotFound()
        {
            // Arrange
            Mock<ForumProjectContext> mockedContext = new Mock<ForumProjectContext>();
            MockedIdentity mockedIdentity = new MockedIdentity();
            ObserveService service = new ObserveService(mockedContext.Object, mockedIdentity.userManagerMock.Object);
            
            var existingTag = new Tag
            {
                TagName = "exampleTag"
            };

            string userId = Guid.NewGuid().ToString();
            string tagName = "exampleTag";
            
            mockedContext.Setup(m => m.AspNetUsers.FindAsync(userId)).ReturnsAsync((ApplicationUser)null);
            mockedContext.Setup(m => m.Tags.FindAsync(tagName)).ReturnsAsync(existingTag);
            // Act
            var result = await service.HandleTagObservation(existingTag.TagName, userId);

            // Assert
            Assert.False(result);
        }
   
        [Fact]
        public async void HandleTagObservation_ShouldReturnFalse_WhenTagIsNotFound()
        {
            // Arrange
            Mock<ForumProjectContext> mockedContext = new Mock<ForumProjectContext>();
            MockedIdentity mockedIdentity = new MockedIdentity();
            ObserveService service = new ObserveService(mockedContext.Object, mockedIdentity.userManagerMock.Object);

            var existingTag = new Tag
            {
                TagName = "exampleTag"
            };

            var existingUser = new ApplicationUser
            {
                UserName = "ExistingUsername",
                Email = "ExistingEmail@gmail.com",
            };

            string userId = Guid.NewGuid().ToString();
            string tagName = "tag";

            mockedContext.Setup(m => m.AspNetUsers.FindAsync(userId)).ReturnsAsync(existingUser);
            mockedContext.Setup(m => m.Tags.FindAsync(tagName)).ReturnsAsync((Tag)null);
            // Act
            var result = await service.HandleTagObservation(existingTag.TagName, userId);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public async void HandleTagObservation_ShouldReturnFalse_WhenBothTagAndUserIsNotFound()
        {
            // Arrange
            Mock<ForumProjectContext> mockedContext = new Mock<ForumProjectContext>();
            MockedIdentity mockedIdentity = new MockedIdentity();
            ObserveService service = new ObserveService(mockedContext.Object, mockedIdentity.userManagerMock.Object);

            var existingTag = new Tag
            {
                TagName = "exampleTag"
            };

            var existingUser = new ApplicationUser
            {
                UserName = "ExistingUsername",
                Email = "ExistingEmail@gmail.com",
            };

            string userId = Guid.NewGuid().ToString();
            string tagName = "tag";

            mockedContext.Setup(m => m.AspNetUsers.FindAsync(userId)).ReturnsAsync((ApplicationUser)null);
            mockedContext.Setup(m => m.Tags.FindAsync(tagName)).ReturnsAsync((Tag)null);
            // Act
            var result = await service.HandleTagObservation(existingTag.TagName, userId);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public async void HandleTagObservation_ShouldReturnTrue_WhenBothTagAndUserIsFound()
        {
            // Arrange
            Mock<ForumProjectContext> mockedContext = new Mock<ForumProjectContext>();
            MockedIdentity mockedIdentity = new MockedIdentity();
            ObserveService service = new ObserveService(mockedContext.Object, mockedIdentity.userManagerMock.Object);
            var existingTag = new Tag
            {
                TagName = "exampleTag"
            };

            var existingUser = new ApplicationUser
            {
                UserName = "ExistingUsername",
                Email = "ExistingEmail@gmail.com",
                TagNames = new List<Tag> 
                {
                    existingTag
                }
            };

            string userId = Guid.NewGuid().ToString();
            string tagName = "exampleTag";

            var users = new List<ApplicationUser>
            {
                existingUser

            }.AsQueryable();

            var mockSet = new Mock<DbSet<ApplicationUser>>();
            mockSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            
            mockedContext.Setup(c => c.AspNetUsers).Returns(mockSet.Object);
           
            bool alreadyObserved = mockedContext.Object.AspNetUsers
                                   .Where(u => u.Id == userId)
                                   .SelectMany(u => u.TagNames)
                                   .Contains(existingTag);

            mockedContext.Setup(m => m.AspNetUsers.FindAsync(userId)).ReturnsAsync(existingUser);
            mockedContext.Setup(m => m.Tags.FindAsync(tagName)).ReturnsAsync(existingTag);
            // Act
            var result = await service.HandleTagObservation(existingTag.TagName, userId);

            // Assert
            Assert.True(result);
        }
    }
}

