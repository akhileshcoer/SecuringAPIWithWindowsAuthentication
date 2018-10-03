using Microsoft.AspNetCore.Authorization;

namespace SecuringAPIWithWindowsAuthentication
{
    public class SinglePolicyRequirement: IAuthorizationRequirement
    {
        public string PolicyName { get; set; }

        public SinglePolicyRequirement(string policyName)
        {
            PolicyName = policyName;
        }
    }
}
