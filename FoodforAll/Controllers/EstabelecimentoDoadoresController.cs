using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodforAll.Models;
using Microsoft.AspNetCore.Authorization;

namespace FoodforAll.Controllers
{

    public class EstabelecimentoDoadoresController : Controller
    {
        private readonly AppDbContext _context;

        public EstabelecimentoDoadoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EstabelecimentoDoadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstabelecimentosDoadores.ToListAsync());
        }

        // GET: EstabelecimentoDoadores/Details/5
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

        // GET: EstabelecimentoDoadores/Create
        [Authorize(Policy = "EstabelecimentoDoador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstabelecimentoDoadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "EstabelecimentoDoador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CNPJ,NomeFantasia,TransporteProprio")] EstabelecimentoDoador estabelecimentoDoador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estabelecimentoDoador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estabelecimentoDoador);
        }

        // GET: EstabelecimentoDoadores/Edit/5
        [Authorize(Policy = "EstabelecimentoDoador")]
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

        [Authorize(Policy = "EstabelecimentoDoador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,CNPJ,NomeFantasia,TransporteProprio")] EstabelecimentoDoador estabelecimentoDoador)
        {
            if (id != estabelecimentoDoador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: EstabelecimentoDoadores/Delete/5
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

        // POST: EstabelecimentoDoadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var estabelecimentoDoador = await _context.EstabelecimentosDoadores.FindAsync(id);
            if (estabelecimentoDoador != null)
            {
                _context.EstabelecimentosDoadores.Remove(estabelecimentoDoador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstabelecimentoDoadorExists(int? id)
        {
            return _context.EstabelecimentosDoadores.Any(e => e.Id == id);
        }
    }
}
