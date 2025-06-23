using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NZWalks.CustomActionFilters;
using NZWalks.Models.DTOs;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }



        // POST: /api/Auth/Register
        //This endpoint registers a user
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Username,
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequest.Password);

            if (identityResult.Succeeded)
            {
                //Add roles to User
                if (registerRequest.Roles != null && registerRequest.Roles.Any())
                {
                        await userManager.AddToRolesAsync(identityUser, registerRequest.Roles);
                        if (identityResult.Succeeded)
                        {
                            return Ok("User was registered successfully");
                        }
                    
                }
            }
            
            return BadRequest(identityResult.Errors);



        }
        
        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.UserName);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (checkPasswordResult)
                {
                    //Create Token
                    return Ok();
                }
            }
            
            return BadRequest("Username or password is incorrect");
        }
        
    }
}
