using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SecuringAPIWithWindowsAuthentication.Controllers
{
    public class Entity
    {
        public string Name { get; set; }
    }

    [Route("api/[controller]")]

    public class ResourceController : ControllerBase
    {
        [HttpGet("myroles")]
        public async Task<IActionResult> GetMyRoles()
        {
            var groups = new List<string>();
            if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.Identity is WindowsIdentity)
            {
                var wi = HttpContext.User.Identity as WindowsIdentity;
                if (wi.Groups != null)
                {
                    foreach (var group in wi.Groups)
                    {
                        try
                        {
                            groups.Add(group.Translate(typeof(NTAccount)).ToString());
                        }
                        catch (Exception)
                        {
                            //
                        }
                    }
                }
            }

            return await Task.FromResult(new ObjectResult(groups));
        }
        
        [Authorize(Roles = "Administrator")]
        [HttpGet("AdminGet")]
        public async Task<IActionResult> AdminGetAsync()
        {
            return await Task.FromResult(new ObjectResult(new Entity() { Name = "administrator role" }));
        }

        [Authorize(Roles = "ALI\\ATIN-FC-Systems-Dev")]
        [HttpGet("OtherGet")]
        public async Task<IActionResult> OtherGetAsync()
        {
            return await Task.FromResult(new ObjectResult(new Entity() { Name = "ALI\\ATIN-FC-Systems-Dev" }));
        }
    }
}
