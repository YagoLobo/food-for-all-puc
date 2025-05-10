using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodForAll.Models;

namespace FoodForAll.Controllers
{
    public class EstabelecimentoReceptorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstabelecimentoReceptorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EstabelecimentoReceptors
        public async Task<IActionResult> Index()
        {
            var receptores = await _context.EstabelecimentoReceptor.Include(e => e.Usuario).ToListAsync();
            return View(receptores);
        }

        // GET: EstabelecimentoReceptors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var receptor = await _context.EstabelecimentoReceptor.Include(e => e.Usuario).FirstOrDefaultAsync(m => m.Id == id);
            if (receptor == null)
                return NotFound();
            return View(receptor);
        }

        // GET: EstabelecimentoReceptors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstabelecimentoReceptors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstabelecimentoReceptor estabelecimentoReceptor, string nomeUsuario, string email, string senha, string contato, string endereco)
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
                    TipoUsuario = TipoUsuario.EstabelecimentoReceptor,
                    Endereco = endereco,
                    Contato = contato
                };
                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();
                // Vincula o usuário ao receptor
                estabelecimentoReceptor.UsuarioId = usuario.Id;
                _context.EstabelecimentoReceptor.Add(estabelecimentoReceptor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estabelecimentoReceptor);
        }

        // GET: EstabelecimentoReceptors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var receptor = await _context.EstabelecimentoReceptor.Include(e => e.Usuario).FirstOrDefaultAsync(m => m.Id == id);
            if (receptor == null)
                return NotFound();
            return View(receptor);
        }

        // POST: EstabelecimentoReceptors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EstabelecimentoReceptor estabelecimentoReceptor)
        {
            if (id != estabelecimentoReceptor.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estabelecimentoReceptor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.EstabelecimentoReceptor.Any(e => e.Id == estabelecimentoReceptor.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estabelecimentoReceptor);
        }

        // GET: EstabelecimentoReceptors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var receptor = await _context.EstabelecimentoReceptor.Include(e => e.Usuario).FirstOrDefaultAsync(m => m.Id == id);
            if (receptor == null)
                return NotFound();
            return View(receptor);
        }

        // POST: EstabelecimentoReceptors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receptor = await _context.EstabelecimentoReceptor.FindAsync(id);
            if (receptor != null)
            {
                _context.EstabelecimentoReceptor.Remove(receptor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 