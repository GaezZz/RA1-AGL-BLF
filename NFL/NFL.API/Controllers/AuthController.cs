using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NFL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //aqui voy a leer lo del appsettings, lo vamos a pasar mejor alli para no poner el login dentro del codigo
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        public class LoginModel
        {
            public string User { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // comprobar las credenciales desde el json
            var validUser = _configuration["Jwt:AdminUser"];
            var validPassword = _configuration["Jwt:AdminPassword"];

           
            if (model.User == validUser && model.Password == validPassword)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                // clave secreta desde el json
                var secretKey = _configuration["Jwt:Key"]!;
                var key = Encoding.ASCII.GetBytes(secretKey);

                // datos del token, quienes somos y la duracion
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.User) }),
                    Expires = DateTime.UtcNow.AddHours(2), //le doamos 2 horas de validez
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                //crear el token y para darselo a las apps
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { token = tokenString });
            }

            // en caso de fallar lo anterior devolvemos un error 
            return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });
        }
    }
}