using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodforAll.Models;

namespace FoodforAll.Controllers
{
    public class EstabelecimentoReceptorsController : Controller
    {
        private readonly AppDbContext _context;

        public EstabelecimentoReceptorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EstabelecimentoReceptors
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstabelecimentosReceptores.ToListAsync());
        }

        // GET: EstabelecimentoReceptors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimentoReceptor = await _context.EstabelecimentosReceptores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estabelecimentoReceptor == null)
            {
                return NotFound();
            }

            return View(estabelecimentoReceptor);
        }

        // GET: EstabelecimentoReceptors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstabelecimentoReceptors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,RegistroGoverno,Aprovada,RefeicoesDiarias,Senha,Email,Telefone,Endereco,CreatedAt,UpdatedAt")] EstabelecimentoReceptor estabelecimentoReceptor)
        {
            if (ModelState.IsValid)
            {
                estabelecimentoReceptor.Senha = BCrypt.Net.BCrypt.HashPassword(estabelecimentoReceptor.Senha);
                _context.Add(estabelecimentoReceptor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estabelecimentoReceptor);
        }

        // GET: EstabelecimentoReceptors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimentoReceptor = await _context.EstabelecimentosReceptores.FindAsync(id);
            if (estabelecimentoReceptor == null)
            {
                return NotFound();
            }
            return View(estabelecimentoReceptor);
        }

        // POST: EstabelecimentoReceptors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,RegistroGoverno,Aprovada,RefeicoesDiarias,Senha,Email,Telefone,Endereco,CreatedAt,UpdatedAt")] EstabelecimentoReceptor estabelecimentoReceptor)
        {
            if (id != estabelecimentoReceptor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    estabelecimentoReceptor.Senha = BCrypt.Net.BCrypt.HashPassword(estabelecimentoReceptor.Senha);
                    _context.Update(estabelecimentoReceptor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstabelecimentoReceptorExists(estabelecimentoReceptor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estabelecimentoReceptor);
        }

        // GET: EstabelecimentoReceptors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimentoReceptor = await _context.EstabelecimentosReceptores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estabelecimentoReceptor == null)
            {
                return NotFound();
            }

            return View(estabelecimentoReceptor);
        }

        // POST: EstabelecimentoReceptors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estabelecimentoReceptor = await _context.EstabelecimentosReceptores.FindAsync(id);
            if (estabelecimentoReceptor != null)
            {
                _context.EstabelecimentosReceptores.Remove(estabelecimentoReceptor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstabelecimentoReceptorExists(int id)
        {
            return _context.EstabelecimentosReceptores.Any(e => e.Id == id);
        }
    }
}
