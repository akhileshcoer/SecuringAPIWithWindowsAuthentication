﻿using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SecuringAPIWithWindowsAuthentication
{
    public class MultiplePolicyOrHandler : AuthorizationHandler<MultiplePolicyOrRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MultiplePolicyOrRequirement requirement)
        {            
            if (context.User.Identity.IsAuthenticated && context.User.Identity is WindowsIdentity)
            {
                foreach (var policy in requirement.Policies)
                {
                    if (context.User.IsInRole(policy.PolicyName))
                    {
                        context.Succeed(requirement);
                        break;
                    } 
                }
            }

            return Task.FromResult(0);
        }
    }
}
