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
    public class PecuaristasController : Controller
    {
        private readonly Contexto _context;

        public PecuaristasController(Contexto context)
        {
            _context = context;
        }

        // GET: Pecuaristas1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pecuaristas.ToListAsync());
        }

        // GET: Pecuaristas1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecuarista = await _context.Pecuaristas
                .FirstOrDefaultAsync(m => m.PecuaristaId == id);
            if (pecuarista == null)
            {
                return NotFound();
            }

            return View(pecuarista);
        }

        // GET: Pecuaristas1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pecuaristas1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PecuaristaId,Nome")] Pecuarista pecuarista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pecuarista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pecuarista);
        }

        // GET: Pecuaristas1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecuarista = await _context.Pecuaristas.FindAsync(id);
            if (pecuarista == null)
            {
                return NotFound();
            }
            return View(pecuarista);
        }

        // POST: Pecuaristas1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PecuaristaId,Nome")] Pecuarista pecuarista)
        {
            if (id != pecuarista.PecuaristaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pecuarista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PecuaristaExists(pecuarista.PecuaristaId))
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
            return View(pecuarista);
        }

        // GET: Pecuaristas1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecuarista = await _context.Pecuaristas
                .FirstOrDefaultAsync(m => m.PecuaristaId == id);
            if (pecuarista == null)
            {
                return NotFound();
            }

            return View(pecuarista);
        }

        // POST: Pecuaristas1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pecuarista = await _context.Pecuaristas.FindAsync(id);
            _context.Pecuaristas.Remove(pecuarista);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PecuaristaExists(int id)
        {
            return _context.Pecuaristas.Any(e => e.PecuaristaId == id);
        }
    }
}
