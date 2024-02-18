using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod = Project_Forum.Models;
namespace Forum_xUnitTests
{
    public class LoginModelUnitTests
    {
        [Theory]
        [InlineData("alice","password")]
        [InlineData("mikey","password")]
        [InlineData("michael123","password")]
        [InlineData("mike1232131sa33", "password")]
        public void LoginModel_ShouldReturnTrue_WhenUsernameLengthValid(params string[] userCreditentials)
        {
            var model = new Mod.LoginModel()
            {
                Username = userCreditentials[0],
                Password = userCreditentials[1],
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Username, 
                new ValidationContext(model) { MemberName="Username"}, 
                validationResults);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("alice", "password")]
        [InlineData("mikey", "password")]
        [InlineData("michael123", "password")]
        [InlineData("mike1232131sa33", "password")]
        public void LoginModel_ShouldReturnTrue_WhenUsernameIsNotNullOrEmpty(params string[] userCreditentials)
        {
            var model = new Mod.LoginModel()
            {
                Username = userCreditentials[0],
                Password = userCreditentials[1],
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Username,
                new ValidationContext(model) { MemberName = "Username" },
                validationResults);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("alice", "Password_2@!*")]
        [InlineData("mikey", "Password123!")]
        [InlineData("michael123", "Validpass_!@3#$")]
        [InlineData("mike1232131sa33", "****1pasSword")]
        public void LoginModel_ShouldReturnTrue_WhenPasswordIsNotNullOrEmpty(params string[] userCreditentials)
        {
            var model = new Mod.LoginModel()
            {
                Username = userCreditentials[0],
                Password = userCreditentials[1],
            };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Password,
                new ValidationContext(model) { MemberName = "Password" },
                validationResults);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("", "password")]
        [InlineData("1234567890qwertyuiopasdfghjklzxcvbnm1", "password")]
        [InlineData("1234567890qwertyuiopasdfghjklzxcvbnm12145", "password")]
        public void LoginModel_ShouldReturnFalse_WhenUsernameLengthInvalid(params string[] userCreditentials)
        {
            var model = new Mod.LoginModel()
            {
                Username = userCreditentials[0],
                Password = userCreditentials[1],
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Username,
                new ValidationContext(model) { MemberName = "Username" },
                validationResults);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("", "password")]
        [InlineData(null, "password")]
        public void LoginModel_ShouldReturnFalse_WhenUsernameIsNullOrEmpty(params string[] userCreditentials)
        {
            var model = new Mod.LoginModel()
            {
                Username = userCreditentials[0],
                Password = userCreditentials[1],
            };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Username,
                new ValidationContext(model) { MemberName = "Username" },
                validationResults);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("login", "")]
        [InlineData("login", null)]
        public void LoginModel_ShouldReturnFalse_WhenPasswordIsNullOrEmpty(params string[] userCreditentials)
        {
            var model = new Mod.LoginModel()
            {
                Username = userCreditentials[0],
                Password = userCreditentials[1],
            };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Password,
                new ValidationContext(model) { MemberName = "Password" },
                validationResults);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("login", "Password123!")]
        [InlineData("login", "ValidPaswd2@")]
        public void LoginModel_ShouldReturnTrue_WhenPasswordAndUsernameIsValid(params string[] userCreditentials)
        {
            var model = new Mod.LoginModel()
            {
                Username = userCreditentials[0],
                Password = userCreditentials[1],
            };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, new ValidationContext(model), validationResults, validateAllProperties: true);
            
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("login", " Valid Paswd2@")]
        [InlineData("login", " ValidPaswd2@")]
        [InlineData("login", " ValidPaswd2@ ")]
        public void LoginModel_ShouldReturnFalse_WhenPasswordContainSpaces(params string[] userCreditentials)
        {
            var model = new Mod.LoginModel()
            {
                Username = userCreditentials[0],
                Password = userCreditentials[1],
            };
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Password,
                new ValidationContext(model) { MemberName = "Password" },
                validationResults);

            Assert.False(isValid);
        }
        [Theory]
        [InlineData(" login", "ValidPaswd2@")]
        [InlineData("lo gin", "ValidPaswd2@")]
        [InlineData("login ", "ValidPaswd2@ ")]
        public void LoginModel_ShouldReturnFalse_WhenUsernameContainSpaces(params string[] userCreditentials)
        {
            var model = new Mod.LoginModel()
            {
                Username = userCreditentials[0],
                Password = userCreditentials[1],
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Username,
                new ValidationContext(model) { MemberName = "Username" },
                validationResults);

            Assert.False(isValid);
        }
    }
}
