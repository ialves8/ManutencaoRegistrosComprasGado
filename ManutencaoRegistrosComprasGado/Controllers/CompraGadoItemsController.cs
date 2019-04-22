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
    public class CompraGadoItemsController : Controller
    {
        private readonly Contexto _context;

        public CompraGadoItemsController(Contexto context)
        {
            _context = context;
        }

        // GET: CompraGadoItems
        public async Task<IActionResult> Index()
        {
            var contexto = _context.CompraGadoItens.Include(c => c.Animal).Include(c => c.CompraGado);
            return View(await contexto.ToListAsync());
        }

        // GET: CompraGadoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compraGadoItem = await _context.CompraGadoItens
                .Include(c => c.Animal)
                .Include(c => c.CompraGado)
                .FirstOrDefaultAsync(m => m.CompraGadoItemId == id);
            if (compraGadoItem == null)
            {
                return NotFound();
            }

            return View(compraGadoItem);
        }

        // GET: CompraGadoItems/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animais, "AnimalId", "Descricao");
            ViewData["CompraGadoId"] = new SelectList(_context.CompraGados, "CompraGadoId", "CompraGadoId");
            return View();
        }

        // POST: CompraGadoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompraGadoItemId,Quantidade,CompraGadoId,AnimalId")] CompraGadoItem compraGadoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compraGadoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animais, "AnimalId", "Descricao", compraGadoItem.AnimalId);
            ViewData["CompraGadoId"] = new SelectList(_context.CompraGados, "CompraGadoId", "CompraGadoId", compraGadoItem.CompraGadoId);
            return View(compraGadoItem);
        }

        // GET: CompraGadoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compraGadoItem = await _context.CompraGadoItens.FindAsync(id);
            if (compraGadoItem == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animais, "AnimalId", "Descricao", compraGadoItem.AnimalId);
            ViewData["CompraGadoId"] = new SelectList(_context.CompraGados, "CompraGadoId", "CompraGadoId", compraGadoItem.CompraGadoId);
            return View(compraGadoItem);
        }

        // POST: CompraGadoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompraGadoItemId,Quantidade,CompraGadoId,AnimalId")] CompraGadoItem compraGadoItem)
        {
            if (id != compraGadoItem.CompraGadoItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compraGadoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraGadoItemExists(compraGadoItem.CompraGadoItemId))
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
            ViewData["AnimalId"] = new SelectList(_context.Animais, "AnimalId", "Descricao", compraGadoItem.AnimalId);
            ViewData["CompraGadoId"] = new SelectList(_context.CompraGados, "CompraGadoId", "CompraGadoId", compraGadoItem.CompraGadoId);
            return View(compraGadoItem);
        }

        // GET: CompraGadoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compraGadoItem = await _context.CompraGadoItens
                .Include(c => c.Animal)
                .Include(c => c.CompraGado)
                .FirstOrDefaultAsync(m => m.CompraGadoItemId == id);
            if (compraGadoItem == null)
            {
                return NotFound();
            }

            return View(compraGadoItem);
        }

        // POST: CompraGadoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compraGadoItem = await _context.CompraGadoItens.FindAsync(id);
            _context.CompraGadoItens.Remove(compraGadoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraGadoItemExists(int id)
        {
            return _context.CompraGadoItens.Any(e => e.CompraGadoItemId == id);
        }
    }
}
