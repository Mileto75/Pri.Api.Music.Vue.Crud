using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pri.Api.Music.Api.Dtos;
using Pri.Api.Music.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pri.Api.Music.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthLoginRequestDto authLoginRequestDto)
        {
            //authenticate the user
            var result = await _signInManager.PasswordSignInAsync
                (authLoginRequestDto.Username,authLoginRequestDto.Password,false,false);
            if(!result.Succeeded)//wrong credentials
            {
                ModelState.AddModelError("", "Wrong credentials!");
                return Unauthorized(ModelState.Values);
            }
            //get the user
            var user = await _userManager.FindByNameAsync(authLoginRequestDto.Username);
            //get the claims
            var claims = await _userManager.GetClaimsAsync(user);
            //generate the token
            //set the token parameters
            var issuer = _configuration.GetValue<string>("JWTConfiguration:Issuer");
            var audience = _configuration.GetValue<string>("JWTConfiguration:Audience");
            var expiration = DateTime.Now.AddDays(_configuration.GetValue<int>("JWTConfiguration:ExpirationInDays"));
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTConfiguration:SecretKey"));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                notBefore: DateTime.Now,
                expires: expiration,
                claims: claims,
                signingCredentials: signinCredentials
                );
            //serialize token
            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
            //return the token
            return Ok(new AuthLoginResponseDto { Token = serializedToken });
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(AuthRegisterRequestDto authRegisterRequestDto)
        {
            //create the user
            var newUser = new ApplicationUser
            {
                UserName = authRegisterRequestDto.Username,
                Firstname = authRegisterRequestDto.Firstname,
                Lastname = authRegisterRequestDto.Lastname,
                DateOfBirth = authRegisterRequestDto.DateOfBirth,
                Email = authRegisterRequestDto.Username,
                EmailConfirmed = true,//ONLY FOR TESTING/DEVELOPMENT PURPOSE
            };
            //add the user
            var result = await _userManager.CreateAsync(newUser,authRegisterRequestDto.Password);
            if(!result.Succeeded)
            {
                //add to the modelstate
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState.Values);
            }
            //add the claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,"User"),
                new Claim(ClaimTypes.DateOfBirth,newUser.DateOfBirth.ToString()),
                new Claim(ClaimTypes.Name,newUser.UserName),
                new Claim(ClaimTypes.NameIdentifier,newUser.Id),
            };
            //add claims to user
            result = await _userManager.AddClaimsAsync(newUser, claims);
            if (!result.Succeeded)
            {
                //add to the modelstate
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState.Values);
            }
            //call the emailservice
            return Ok("Registered");
        }

    }
}
