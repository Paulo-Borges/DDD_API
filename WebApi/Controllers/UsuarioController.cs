using Aplicacao.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Token;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IAplicacaoUsuario _aplicacaoUsuario;
        public UsuarioController(IAplicacaoUsuario aplicacaoUsuario)
        {
            _aplicacaoUsuario = aplicacaoUsuario;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarToken")]

        public async Task<IActionResult> CriarToken([FromBody] Login login)
        {
            if(string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            return Unauthorized();

            var resultado = await _aplicacaoUsuario.ExisteUsuario(login.email, login.senha);
            if (resultado)
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Empresa - Borges Dev")
                    .AddIssuer("Teste.Securiry.Bearer")
                    .AddAudience("Teste.Securiry.Bearer")
                    .AddClaim("UsuarioAPINumero", "1")
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }

        }

    }
}
