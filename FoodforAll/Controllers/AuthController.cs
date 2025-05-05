using Microsoft.AspNetCore.Mvc;
using FoodforAll.Models;
using FoodforAll.Models.ViewModels;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace FoodforAll.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            if (model.Tipo == TipoLogin.Doador)
            {
                var usuario = await _context.EstabelecimentosDoadores.FirstOrDefaultAsync(e => e.Email == model.Email);
                if (usuario == null)
                     ModelState.AddModelError("", "Doador não encontrado ou senha incorreta.");
                else
                {
                    var senhaOk = BCrypt.Net.BCrypt.Verify(model.Senha, usuario.Senha);
                    if (senhaOk)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuario.NomeFantasia),
                            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                            new Claim(ClaimTypes.Role, TipoLogin.Doador.ToString())
                        };

                        var usuarioIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdentity);

                        var props = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            ExpiresUtc = DateTime.UtcNow.ToLocalTime().AddHours(8),
                            IsPersistent = true,
                        };

                        await HttpContext.SignInAsync(principal, props);

                        return RedirectToAction("Index", "EstabelecimentoDoadors");
                    }
                    else
                        ModelState.AddModelError("", "Doador não encontrado ou senha incorreta.");
                }
            }
            else
            {
                var usuario = await _context.EstabelecimentosReceptores.FirstOrDefaultAsync(e => e.Email == model.Email);
                if (usuario == null)
                    ModelState.AddModelError("", "Doador não encontrado ou senha incorreta.");
                else
                {
                    var senhaOk = BCrypt.Net.BCrypt.Verify(model.Senha, usuario.Senha);
                    if (senhaOk)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuario.Nome),
                            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                            new Claim(ClaimTypes.Role, TipoLogin.Doador.ToString())
                        };

                        var usuarioIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdentity);

                        var props = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            ExpiresUtc = DateTime.UtcNow.ToLocalTime().AddHours(8),
                            IsPersistent = true,
                        };

                        await HttpContext.SignInAsync(principal, props);

                        return RedirectToAction("Index", "EstabelecimentosReceptors");
                    }
                    else
                        ModelState.AddModelError("", "Doador não encontrado ou senha incorreta.");
                }
            }

            return View(model);
        }
    }
}

