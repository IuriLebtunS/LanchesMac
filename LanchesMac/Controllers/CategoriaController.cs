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
        public async Task<IActionResult> Create(Categoria model)
        {
            if (!ModelState.IsValid)
                return View(model);


            await _context.Categorias.AddAsync(model);

            await _context.SaveChangesAsync();

            return View("_CadastradoComSucesso");

        }
        
        [HttpGet]
        public async Task<IActionResult>Edit(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(Categoria model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(List));
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>Delete(int id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(m => m.CategoriaId == id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult>DeleteConfirmed(Categoria model)
        {
            
            _context.Categorias.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }
        
    }

}