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
    public class EstabelecimentoDoadorsController : Controller
    {
        private readonly AppDbContext _context;

        public EstabelecimentoDoadorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EstabelecimentoDoadors
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstabelecimentosDoadores.ToListAsync());
        }

        // GET: EstabelecimentoDoadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimentoDoador = await _context.EstabelecimentosDoadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estabelecimentoDoador == null)
            {
                return NotFound();
            }

            return View(estabelecimentoDoador);
        }

        // GET: EstabelecimentoDoadors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstabelecimentoDoadors/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CNPJ,NomeFantasia,Categoria,Senha,Email,Telefone,Endereco,CreatedAt,UpdatedAt")] EstabelecimentoDoador estabelecimentoDoador)
        {
            if (ModelState.IsValid)
            {
                estabelecimentoDoador.Senha = BCrypt.Net.BCrypt.HashPassword(estabelecimentoDoador.Senha);
                _context.Add(estabelecimentoDoador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estabelecimentoDoador);
        }

        // GET: EstabelecimentoDoadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimentoDoador = await _context.EstabelecimentosDoadores.FindAsync(id);
            if (estabelecimentoDoador == null)
            {
                return NotFound();
            }
            return View(estabelecimentoDoador);
        }

        // POST: EstabelecimentoDoadors/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CNPJ,NomeFantasia,Categoria,Senha,Email,Telefone,Endereco,CreatedAt,UpdatedAt")] EstabelecimentoDoador estabelecimentoDoador)
        {
            if (id != estabelecimentoDoador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    estabelecimentoDoador.Senha = BCrypt.Net.BCrypt.HashPassword(estabelecimentoDoador.Senha);
                    _context.Update(estabelecimentoDoador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstabelecimentoDoadorExists(estabelecimentoDoador.Id))
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
            return View(estabelecimentoDoador);
        }

        // GET: EstabelecimentoDoadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimentoDoador = await _context.EstabelecimentosDoadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estabelecimentoDoador == null)
            {
                return NotFound();
            }

            return View(estabelecimentoDoador);
        }

        // POST: EstabelecimentoDoadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estabelecimentoDoador = await _context.EstabelecimentosDoadores.FindAsync(id);
            if (estabelecimentoDoador != null)
            {
                _context.EstabelecimentosDoadores.Remove(estabelecimentoDoador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstabelecimentoDoadorExists(int id)
        {
            return _context.EstabelecimentosDoadores.Any(e => e.Id == id);
        }
    }
}
