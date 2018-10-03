using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SecuringAPIWithWindowsAuthentication
{
    public class SinglePolicyHandler : AuthorizationHandler<SinglePolicyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SinglePolicyRequirement requirement)
        {
            string policyName = requirement.PolicyName;
            if (context.User.Identity.IsAuthenticated && context.User.Identity is WindowsIdentity)
            {
                if (context.User.IsInRole(requirement.PolicyName))
                {
                    context.Succeed(requirement);
                }
            }
            
            return Task.FromResult(0);
        }
    }
}
