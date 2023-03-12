using ApiGestionPersonas.Dtos;
using ApiGestionPersonas.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static ApiGestionPersonas.Repositories.AutenticacionDataBaseContext;

namespace ApiGestionPersonas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : Controller
    {
        private readonly string SecretKey;
        private readonly AutenticacionDataBaseContext _autenticacionDataBaseContext;

        public AutenticacionController(IConfiguration config, AutenticacionDataBaseContext autenticacionDataBaseContext)
        {
            SecretKey = config.GetSection("Settings").GetSection("SecretKey").ToString()!;
            _autenticacionDataBaseContext = autenticacionDataBaseContext;
        }

        [HttpPost("Acceso")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioEntityLogin))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Acceso([FromBody] UsuarioAccesoDto usuario)
        {
            var user = await _autenticacionDataBaseContext.Get(usuario);
            if (user == null)
                return new UnauthorizedObjectResult("Credenciales invalidas");
            return new OkObjectResult(user);
        }


        [HttpPost("ObtenerToken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerarToken([FromBody] UsuarioDto user)
        {
            var KeyBytes = Encoding.ASCII.GetBytes(SecretKey);
            var claims = new ClaimsIdentity();
            var perfil = await _autenticacionDataBaseContext.Get(user.Id);
            if (perfil == null) return new BadRequestResult();

            claims.AddClaim(new Claim("Username", user.Username));
            claims.AddClaim(new Claim("Perfil", perfil.Nombre));

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var TokenHandle = new JwtSecurityTokenHandler();
            var TokenConfig = TokenHandle.CreateToken(TokenDescriptor);

            string TokenCreado = TokenHandle.WriteToken(TokenConfig);

            return new OkObjectResult(TokenCreado);
        }
    }
}
