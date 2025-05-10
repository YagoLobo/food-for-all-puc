using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FoodForAll.Models;
using FoodForAll.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace FoodForAll.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Login GET
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // Login POST
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
        // Login simples: salva nome do usuário na sessão
        HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
        TempData["Success"] = $"Bem-vindo, {usuario.Nome}!";
        return RedirectToAction("Index");
    }

    // Register GET
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // Register POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Verifica se o e-mail já está cadastrado
        if (await _context.Usuario.AnyAsync(u => u.Email == model.Email))
        {
            ModelState.AddModelError("Email", "E-mail já cadastrado.");
            return View(model);
        }

        // Hash da senha simples (não recomendado para produção)
        string salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: model.Password,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32));

        var usuario = new Usuario
        {
            Nome = model.Nome,
            Email = model.Email,
            PasswordHash = hash + ":" + salt,
            TipoUsuario = model.UserType == "Supermercado" ? TipoUsuario.EstabelecimentoDoador : TipoUsuario.EstabelecimentoReceptor,
            Endereco = "Preencher depois",
            Contato = "Preencher depois"
        };
        _context.Usuario.Add(usuario);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Cadastro realizado com sucesso! Faça login.";
        return RedirectToAction("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
