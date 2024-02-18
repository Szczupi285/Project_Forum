using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod = Project_Forum.Models;

namespace Forum_xUnitTests
{
    public class RegisterModelUnitTests
    {
        #region USERNAME_AND_PASSWORD
        [Theory]
        [InlineData("alice", "password")]
        [InlineData("mikey", "password")]
        [InlineData("michael123", "password")]
        [InlineData("mike1232131sa33", "password")]
        public void RegisterModel_ShouldReturnTrue_WhenUsernameLengthValid(params string[] userCreditentials)
        {
            var model = new Mod.RegisterModel()
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
        [InlineData("alice", "password")]
        [InlineData("mikey", "password")]
        [InlineData("michael123", "password")]
        [InlineData("mike1232131sa33", "password")]
        public void RegisterModel_ShouldReturnTrue_WhenUsernameIsNotNullOrEmpty(params string[] userCreditentials)
        {
            var model = new Mod.RegisterModel()
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
        public void RegisterModel_ShouldReturnTrue_WhenPasswordIsNotNullOrEmpty(params string[] userCreditentials)
        {
            var model = new Mod.RegisterModel()
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
        public void RegisterModel_ShouldReturnFalse_WhenUsernameLengthInvalid(params string[] userCreditentials)
        {
            var model = new Mod.RegisterModel()
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
        public void RegisterModel_ShouldReturnFalse_WhenUsernameIsNullOrEmpty(params string[] userCreditentials)
        {
            var model = new Mod.RegisterModel()
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
        public void RegisterModel_ShouldReturnFalse_WhenPasswordIsNullOrEmpty(params string[] userCreditentials)
        {
            var model = new Mod.RegisterModel()
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
        [InlineData("login", " Valid Paswd2@")]
        [InlineData("login", " ValidPaswd2@")]
        [InlineData("login", " ValidPaswd2@ ")]
        public void RegisterModel_ShouldReturnFalse_WhenPasswordContainSpaces(params string[] userCreditentials)
        {
            var model = new Mod.RegisterModel()
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
        public void RegisterModel_ShouldReturnFalse_WhenUsernameContainSpaces(params string[] userCreditentials)
        {
            var model = new Mod.RegisterModel()
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
        #endregion

        #region EMAIL
        [Theory]
        [InlineData("exampleEmail@gmail.com")]
        [InlineData("exampleEmail@yahoo.com")]
        [InlineData("exampleEmail@onet.pl")]
        [InlineData("exampleEmail@protonmail.ch")]
        public void RegisterModel_ShouldReturnTrue_WhenValidEmail(string email)
        {
            var model = new Mod.RegisterModel()
            {
                Email = email
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Email,
                new ValidationContext(model) { MemberName = "Email" },
                validationResults);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("exampleEmail@gmail.com ")]
        [InlineData(" exampleEmail@yahoo.com")]
        [InlineData("exampleEm ail@onet.pl")]
        [InlineData("examp leE mail @pro tonm ail.ch")]
        public void RegisterModel_ShouldReturnFalse_WhenEmailContainSpaces(string email)
        {
            var model = new Mod.RegisterModel()
            {
                Email = email
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Email,
                new ValidationContext(model) { MemberName = "Email" },
                validationResults);

            Assert.False(isValid);
        }


        [Theory]
        [InlineData("exampleEmail@gmail")]
        [InlineData("exampleEmail@yahoo")]
        [InlineData("exampleEmail@onet")]
        [InlineData("exampleEmail@protonmail")]
        public void RegisterModel_ShouldReturnFalse_WhenEmailDoesntHaveTopLevelDomain(string email)
        {
            var model = new Mod.RegisterModel()
            {
                Email = email
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Email,
                new ValidationContext(model) { MemberName = "Email" },
                validationResults);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("exampleEmailgmail.com")]
        [InlineData("exampleEmailyahoo.com")]
        [InlineData("exampleEmailonet.pl")]
        [InlineData("exampleEmailprotonmail.ch")]
        public void RegisterModel_ShouldReturnFalse_WhenEmailDoesntHaveAtSymbol(string email)
        {
            var model = new Mod.RegisterModel()
            {
                Email = email
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Email,
                new ValidationContext(model) { MemberName = "Email" },
                validationResults);

            Assert.False(isValid);
        }
        // 
        [Theory]
        [InlineData("exampleEmail@gmail.c")]
        [InlineData("exampleEmail@yahoo.c")]
        [InlineData("exampleEmail@onet.p")]
        [InlineData("exampleEmail@protonmail.c")]
        public void RegisterModel_ShouldReturnFalse_WhenTopLevelDomainIsTooShort(string email)
        {
            var model = new Mod.RegisterModel()
            {
                Email = email
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateProperty(model.Email,
                new ValidationContext(model) { MemberName = "Email" },
                validationResults);

            Assert.False(isValid);
        }
        #endregion
    }
}
