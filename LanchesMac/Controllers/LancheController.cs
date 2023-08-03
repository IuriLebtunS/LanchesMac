using LanchesMac.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

public class LancheController : Controller
{
    private readonly AppDbContext _context;

    public LancheController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> List()
    {
        var lanches = await _context.Lanches.ToListAsync();
        return View(lanches);
    }

    public async Task<IActionResult> Create()
    {
        var categorias = await _context.Lanches.ToListAsync();


        ViewData["ListaDeCategorias"] = new SelectList(categorias, "CategoriaId", "CategoriaNome");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Lanche model)
    {
        if (!ModelState.IsValid)
            return View(model);


        await _context.Lanches.AddAsync(model);

        await _context.SaveChangesAsync();

        return View("_CadastradoComSucesso");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var categoria = await _context.Lanches.FindAsync(id);

        if (categoria == null)
            return NotFound();

        return View(categoria);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Categoria model)
    {
        if (ModelState.IsValid)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }
        return View();
    }
    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(m => m.CategoriaId == id);

        if (categoria == null)
            return NotFound();

        return View(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(Categoria model)
    {

        _context.Categorias.Remove(model);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(List));
    }

}
