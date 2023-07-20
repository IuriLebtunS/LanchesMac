using LanchesMac.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            var lista = await _context.Categorias.ToListAsync();
            return View(lista);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string categoriaNome, string descricao)
        {
            var categoria = new Categoria(categoriaNome, descricao);

            await _context.Categorias.AddAsync(categoria);

            await _context.SaveChangesAsync();

            return View("_CadastradoComSucesso");
            
        }

        public async Task<IActionResult> Edit(int id)
        {
            var publicador = await _context.Categorias.FindAsync(id);

            if (publicador == null)
                return NotFound();

            return View(publicador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Categoria categorias)

        {
            if (ModelState.IsValid)
            {
                _context.Update(categorias);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(categorias);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var publicador = await _context.Categorias.FirstOrDefaultAsync(m => m.CategoriaId == id);

            if (publicador == null)
                return NotFound();

            return View(publicador);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
    
}