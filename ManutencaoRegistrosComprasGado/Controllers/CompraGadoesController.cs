using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManutencaoRegistrosComprasGado.Models;

namespace ManutencaoRegistrosComprasGado.Controllers
{
    public class CompraGadoesController : Controller
    {
        private readonly Contexto _context;

        public CompraGadoesController(Contexto context)
        {
            _context = context;
        }

        // GET: CompraGadoes
        public async Task<IActionResult> Index()
        {
            var contexto = _context.CompraGados.Include(c => c.Pecuarista);
            return View(await contexto.ToListAsync());
        }

        // GET: CompraGadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compraGado = await _context.CompraGados
                .Include(c => c.Pecuarista)
                .FirstOrDefaultAsync(m => m.CompraGadoId == id);
            if (compraGado == null)
            {
                return NotFound();
            }

            return View(compraGado);
        }

        // GET: CompraGadoes/Create
        public IActionResult Create()
        {
            ViewData["PecuaristaId"] = new SelectList(_context.Pecuaristas, "PecuaristaId", "Nome");
            return View();
        }

        // POST: CompraGadoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompraGadoId,DataEntrega,PecuaristaId")] CompraGado compraGado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compraGado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PecuaristaId"] = new SelectList(_context.Pecuaristas, "PecuaristaId", "Nome", compraGado.PecuaristaId);
            return View(compraGado);
        }

        // GET: CompraGadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compraGado = await _context.CompraGados.FindAsync(id);
            if (compraGado == null)
            {
                return NotFound();
            }
            ViewData["PecuaristaId"] = new SelectList(_context.Pecuaristas, "PecuaristaId", "Nome", compraGado.PecuaristaId);
            return View(compraGado);
        }

        // POST: CompraGadoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompraGadoId,DataEntrega,PecuaristaId")] CompraGado compraGado)
        {
            if (id != compraGado.CompraGadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compraGado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraGadoExists(compraGado.CompraGadoId))
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
            ViewData["PecuaristaId"] = new SelectList(_context.Pecuaristas, "PecuaristaId", "Nome", compraGado.PecuaristaId);
            return View(compraGado);
        }

        // GET: CompraGadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compraGado = await _context.CompraGados
                .Include(c => c.Pecuarista)
                .FirstOrDefaultAsync(m => m.CompraGadoId == id);
            if (compraGado == null)
            {
                return NotFound();
            }

            return View(compraGado);
        }

        // POST: CompraGadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compraGado = await _context.CompraGados.FindAsync(id);
            _context.CompraGados.Remove(compraGado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraGadoExists(int id)
        {
            return _context.CompraGados.Any(e => e.CompraGadoId == id);
        }
    }
}
