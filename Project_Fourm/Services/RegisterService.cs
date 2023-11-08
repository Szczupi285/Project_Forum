﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project_.Models;
using Project_Forum.Models;
using System.CodeDom;

        namespace Project_Forum.Services
        {
            public class RegisterService : IRegisterService
            {
                public async Task<bool> RegisterUser(UserManager<ApplicationUser> userManager, RegisterModel model, ModelStateDictionary modelState)
                {
                    var doesEmailExist = await userManager.FindByEmailAsync(model.Email);
                    var doesUsernameExist = await userManager.FindByNameAsync(model.Username);
                    
                    if(doesEmailExist != null && doesUsernameExist != null)
                    {
                        modelState.AddModelError("Email", "Email already taken");
                        modelState.AddModelError("Username", "Username already taken");
                    }
                    else if (doesEmailExist != null)
                    {
                        modelState.AddModelError("Email", "E-Mail already taken");

                    }
                    else if (doesUsernameExist != null)
                    {
                        modelState.AddModelError("Username", "Username already taken");

                    }
                    
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = model.Username,
                        Email = model.Email,
                        DateOfBirth = model.Date,

                    };

                    var result = await userManager.CreateAsync(user, model.Password);

                    return result.Succeeded;
                 
                  
                }
            }
        }
