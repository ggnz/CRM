using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using OpenSaludSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSaludSecurity.Authorization
{
    public class CitaIsOwnerAuthorizationHandler
                        : AuthorizationHandler<OperationAuthorizationRequirement, Cita>
    {
        UserManager<IdentityUser> _userManager;

        public CitaIsOwnerAuthorizationHandler(UserManager<IdentityUser>
    userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                     Cita resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for CRUD permission, return.

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.Clinica?.IdRepresentante != null && resource.Clinica.IdRepresentante == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
