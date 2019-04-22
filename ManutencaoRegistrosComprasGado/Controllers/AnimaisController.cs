using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManutencaoRegistrosComprasGado.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ManutencaoRegistrosComprasGado.Controllers
{
    public class AnimaisController : Controller
    {
        private readonly Contexto _context;

        HttpClient client = new HttpClient();

        //https://webapimrcg20190420122547.azurewebsites.net/api/v1/Pecuaristas
        public AnimaisController(Contexto context)
        {
            _context = context;
            client.BaseAddress = new Uri("https://webapimrcg20190420122547.azurewebsites.net/api/v1/Animais");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
        }
               
        // GET: Animais
        public ActionResult Index()
        {
            //return View(await _context.Animais.ToListAsync());


            //List<Medico> medicos = new List<Medico>();

            var pec = new Pecuarista();

            HttpResponseMessage response = client.GetAsync("api/v1/Animais").Result;
            if (response.IsSuccessStatusCode)
            {
              pec = response.Content.ReadAsAsync<Pecuarista>().Result;
            }
            return View(pec);
        }

        // GET: Animais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animais
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,Descricao,Preco")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: Animais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animais.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // POST: Animais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Descricao,Preco")] Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalId))
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
            return View(animal);
        }

        // GET: Animais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animais
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animais.FindAsync(id);
            _context.Animais.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animais.Any(e => e.AnimalId == id);
        }
    }
}
