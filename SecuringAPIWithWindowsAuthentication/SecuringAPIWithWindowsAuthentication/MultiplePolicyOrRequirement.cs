using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace SecuringAPIWithWindowsAuthentication
{
    public class MultiplePolicyOrRequirement : IAuthorizationRequirement
    {
        public IList<SinglePolicyRequirement> Policies { get; set; }

        public MultiplePolicyOrRequirement(IList<SinglePolicyRequirement> policies)
        {
            Policies = policies;
        }
    }
}
