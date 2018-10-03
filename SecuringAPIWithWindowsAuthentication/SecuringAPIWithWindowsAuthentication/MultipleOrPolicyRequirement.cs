using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace SecuringAPIWithWindowsAuthentication
{
    public class MultipleOrPolicyRequirement : IAuthorizationRequirement
    {
        public IList<SinglePolicyRequirement> Policies { get; set; }

        public MultipleOrPolicyRequirement(IList<SinglePolicyRequirement> policies)
        {
            Policies = policies;
        }
    }
}
