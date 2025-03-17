using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ProductManagerAPI.Models;
using ProductManagerAPI.Services;
using ProductManagerAPI.DTOs;
using Azure;

namespace ProductManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration
,
            ILogger<AuthController> logger)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        [Authorize(Policy = "SuperAdministrador")]
        [HttpPost]
        [Route("create-role")]
        public async Task<IActionResult> CreateRole(string role)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(role));

                if (roleResult.Succeeded)
                {
                    _logger.LogInformation(1, "Role adicionada");
                    return StatusCode(StatusCodes.Status201Created,
                                      new { Status = "Success", Message = $"Role {role} adicionada com sucesso!" }
                                      );
                }
                else
                {
                    _logger.LogInformation(2, "Falha ao adicionar Role");
                    return StatusCode(StatusCodes.Status400BadRequest,
                                      new { Status = "Success", Message = $"Role {role} não adicionada!" }
                                     );
                }
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                               new{Status = "Success", Message = $"Role {role} não adicionada!" }
                             );      
        }

        [Authorize(Policy = "SuperAdministrador")]
        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, $"User {user.Email} added to the {roleName} role");
                    return StatusCode(StatusCodes.Status200OK,
                           new 
                           {
                               Status = "Success",
                               Message =
                           $"User {user.Email} added to the {roleName} role"
                           });
                }
                else
                {
                    _logger.LogInformation(1, $"Error: Unable to add user {user.Email} to the {roleName} role");
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        Status = "Error",
                        Message = $"Error: Unable to add user {user.Email} to the {roleName} role"
                    });
                }
            }
            return BadRequest(new { error = "Unable to find user" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.UserName);

            if (user is not null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim("id",user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

                var refreshToken = _tokenService.GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                             out int refreshTokenValidityInMinutes);

                user.RefreshTokenExpireTime.AddMinutes(refreshTokenValidityInMinutes);

                user.RefreshToken = refreshToken;

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
            //return Forbid();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var userExists = await _userManager.FindByNameAsync(registerDTO.UserName);

            if (userExists != null) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new  { Status = "Error", Message = "User already exists!" });
            }

            ApplicationUser user = new()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password!);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       new { Status = "Error", Message = "User creation failed." });
            }

            return Ok(new { Status = "Success", Message = "User created successfully!" });
        }


        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenDTO tokenModel)
        {

            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken
                                  ?? throw new ArgumentNullException(nameof(tokenModel));

            string? refreshToken = tokenModel.RefreshToken
                                   ?? throw new ArgumentException(nameof(tokenModel));

            //pega a ClaimsPrincipal do token expirado
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

            if (principal == null)
            {
                return BadRequest("Invalid access token/refresh token");
            }

            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username!);

            //verifica se usuário foi encontrado e se o refreshtoken passado é o mesmo do banco e se o refreshtoken n expirou
            if (user == null || user.RefreshToken != refreshToken
                             || user.RefreshTokenExpireTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token/refresh token");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(
                                               principal.Claims.ToList(), _configuration);

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [Authorize(Policy = "SuperAdministrador")]
        [HttpPost]
        [Route("revoke/{userName}")]
        public async Task<IActionResult> Revoke(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) {
                return NotFound("Usuario não encontrado!");
            }

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return NoContent();
        }
    }
}
