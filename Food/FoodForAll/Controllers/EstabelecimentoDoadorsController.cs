using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodForAll.Models;

namespace FoodForAll.Controllers
{
    public class EstabelecimentoDoadorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstabelecimentoDoadorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EstabelecimentoDoadors
        public async Task<IActionResult> Index()
        {
            var doadores = await _context.EstabelecimentoDoador.Include(e => e.Usuario).ToListAsync();
            return View(doadores);
        }

        // GET: EstabelecimentoDoadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var doador = await _context.EstabelecimentoDoador.Include(e => e.Usuario).FirstOrDefaultAsync(m => m.Id == id);
            if (doador == null)
                return NotFound();
            return View(doador);
        }

        // GET: EstabelecimentoDoadors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstabelecimentoDoadors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstabelecimentoDoador estabelecimentoDoador, string nomeUsuario, string email, string senha, string contato, string endereco)
        {
            if (ModelState.IsValid)
            {
                // Criação do usuário vinculado
                var salt = Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(16));
                var hash = Convert.ToBase64String(Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
                    password: senha,
                    salt: Convert.FromBase64String(salt),
                    prf: Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 32));
                var usuario = new Usuario
                {
                    Nome = nomeUsuario,
                    Email = email,
                    PasswordHash = $"{hash}:{salt}",
                    TipoUsuario = TipoUsuario.EstabelecimentoDoador,
                    Endereco = endereco,
                    Contato = contato
                };
                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();
                // Vincula o usuário ao doador
                estabelecimentoDoador.UsuarioId = usuario.Id;
                _context.EstabelecimentoDoador.Add(estabelecimentoDoador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estabelecimentoDoador);
        }

        // GET: EstabelecimentoDoadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var doador = await _context.EstabelecimentoDoador.Include(e => e.Usuario).FirstOrDefaultAsync(m => m.Id == id);
            if (doador == null)
                return NotFound();
            return View(doador);
        }

        // POST: EstabelecimentoDoadors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EstabelecimentoDoador estabelecimentoDoador)
        {
            if (id != estabelecimentoDoador.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estabelecimentoDoador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.EstabelecimentoDoador.Any(e => e.Id == estabelecimentoDoador.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estabelecimentoDoador);
        }

        // GET: EstabelecimentoDoadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var doador = await _context.EstabelecimentoDoador.Include(e => e.Usuario).FirstOrDefaultAsync(m => m.Id == id);
            if (doador == null)
                return NotFound();
            return View(doador);
        }

        // POST: EstabelecimentoDoadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doador = await _context.EstabelecimentoDoador.FindAsync(id);
            if (doador != null)
            {
                _context.EstabelecimentoDoador.Remove(doador);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 