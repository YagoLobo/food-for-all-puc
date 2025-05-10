using Microsoft.AspNetCore.Mvc;
using FoodForAll.Models;
using FoodForAll.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace FoodForAll.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (usuario == null)
            {
                ModelState.AddModelError("Email", "Usuário ou senha inválidos.");
                return View(model);
            }
            // Recupera hash e salt
            var parts = usuario.PasswordHash.Split(':');
            if (parts.Length != 2)
            {
                ModelState.AddModelError("Password", "Senha inválida.");
                return View(model);
            }
            var hash = parts[0];
            var salt = parts[1];
            var hashTentativa = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: model.Password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));
            if (hash != hashTentativa)
            {
                ModelState.AddModelError("Password", "Usuário ou senha inválidos.");
                return View(model);
            }
            // Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario.ToString())
            };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTime.UtcNow.AddHours(8)
            };
            await HttpContext.SignInAsync("CookieAuth", principal, props);
            TempData["Success"] = $"Bem-vindo, {usuario.Nome}!";
            // Redireciona conforme o tipo de usuário
            if (usuario.TipoUsuario == TipoUsuario.EstabelecimentoDoador)
                return RedirectToAction("Doador", "Dashboard");
            else if (usuario.TipoUsuario == TipoUsuario.EstabelecimentoReceptor)
                return RedirectToAction("Receptor", "Dashboard");
            else
                return RedirectToAction("Admin", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }
    }
} 