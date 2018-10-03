using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringAPIWithWindowsAuthentication
{
    public class CustomAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _authorizationOptions;

        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            _authorizationOptions = options.Value;
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policies = policyName.Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (policies.Length > 1)
            {
                var singlePolicies = new List<SinglePolicyRequirement>();
                foreach (var item in policies)
                {
                    foreach (var requirement in _authorizationOptions.GetPolicy(item.Trim()).Requirements)
                    {
                        if (requirement is SinglePolicyRequirement)
                            singlePolicies.Add(requirement as SinglePolicyRequirement);
                    }
                }

                var combineAuthPolicy = new AuthorizationPolicy(
                    new List<IAuthorizationRequirement>
                    {
                        new MultipleOrPolicyRequirement(singlePolicies)
                    },
                    new List<string>());


                return Task.FromResult(combineAuthPolicy);
            }
            else
            {
                return Task.FromResult<AuthorizationPolicy>(_authorizationOptions.GetPolicy(policyName));
            }

        }
    }
}
