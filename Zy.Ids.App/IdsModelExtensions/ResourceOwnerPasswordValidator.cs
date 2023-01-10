using IdentityServer4.Validation;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Services;
using IdentityServer4.Events;
using Zy.User.DAL.Entitys;

namespace Singnalr.DAL.IdentityExentions
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly SignInManager<UserEntity> _signInManager;

        private readonly IEventService _events;

        private readonly UserManager<UserEntity> _userManager;

        public ResourceOwnerPasswordValidator(SignInManager<UserEntity> userStore,
            UserManager<UserEntity> _userManager, IEventService _events)
        {
            this._signInManager = userStore;
            this._events = _events;
            this._userManager = _userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                await ValidateAsyncCore(context);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private async Task RaiseInValidUser(ResourceOwnerPasswordValidationContext context, SignInResult result)
        {
            if (result.IsLockedOut)
            {
                await this._events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "IsLockedOut", false));
            }
            else if (result.IsNotAllowed)
            {
                await this._events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "IsNotAllowed", false));
            }
            else
            {
                await this._events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "invalid credentials", false));
            }
        }

        private async Task ValidateAsyncCore(ResourceOwnerPasswordValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(context.UserName) || string.IsNullOrWhiteSpace(context.Password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
                return;
            }

            UserEntity user = await this._userManager.FindByNameAsync(context.UserName);
            
            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            var result = await this._signInManager.CheckPasswordSignInAsync(user, context.Password, true);

            if (!result.Succeeded)
            {
                await RaiseInValidUser(context, result);
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }
        }
    }

}
