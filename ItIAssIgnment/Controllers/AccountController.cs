using ItIAssIgnment.DTO;
using ItIAssIgnment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ItIAssIgnment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManger;
        public AccountController(UserManager<ApplicationUser> userManger)
        {
            this.userManger = userManger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Registeration(RegisterUserDto userDto)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = userDto.Name;
                user.Email = userDto.Email;
                IdentityResult result = await userManger.CreateAsync(user,userDto.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Created Successfully");
                }
                else
                {
                    return BadRequest(result.Errors.FirstOrDefault().ToString()+"sawy");
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = await userManger.FindByNameAsync(userDto.UserName);
                if(user != null)
                {
                    bool found = await userManger.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {
                        var Claims = new List<Claim>();
                        Claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id));
                        Claims.Add(new Claim(ClaimTypes.Name,user.UserName));
                        Claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
                        var roles = await userManger.GetRolesAsync(user);
                        foreach(var item in roles)
                        {
                            Claims.Add(new Claim(ClaimTypes.Role, item));
                        }
                        SecurityKey sKey = 
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A_Strong_Secure_SecretKey_That_Is_32Bytes_Long"));
                        SigningCredentials SigninCred =
                            new SigningCredentials(sKey, SecurityAlgorithms.HmacSha256);
                         JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: "https://localhost:7287/",                    //url wep Api
                            audience:"http://localhost4200/" ,                       //consumer wep api
                            claims: Claims,
                            expires:DateTime.Now.AddHours(1),
                            signingCredentials: SigninCred
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo
                        }) ;
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                return Unauthorized();
            }
            return Unauthorized();

        }
    }
}
