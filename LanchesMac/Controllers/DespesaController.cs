using LanchesMac.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LanchesMac.Controllers
{
    public class DespesaController : Controller
    {
        private readonly AppDbContext _context;

        public DespesaController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            var lista = await _context.Despesas.ToListAsync();
            return View(lista);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Create()
        {
            ViewData["listadeCategorias"] = new SelectList(await _context.Categorias.ToListAsync(), "CategoriaId", "CategoriaNome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Despesa model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.DataDespesa = DateTime.SpecifyKind(model.DataDespesa, DateTimeKind.Utc);
            await _context.Despesas.AddAsync(model);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var despesa = await _context.Despesas.FindAsync(id);

            if (despesa == null)
                return NotFound();

            ViewData["listadeCategorias"] = new SelectList(await _context.Categorias.ToListAsync(), "CategoriaId", "CategoriaNome");
            return View(despesa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Despesa model)
        {
            if (ModelState.IsValid)
            {
                model.DataDespesa = DateTime.SpecifyKind(model.DataDespesa, DateTimeKind.Utc);
                _context.Update(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(List));
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var despesa = await _context.Despesas.FirstOrDefaultAsync(m => m.DespesaId == id);

            if (despesa == null)
                return NotFound();

            return View(despesa);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Despesa model)
        {

            _context.Despesas.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }

    }

}