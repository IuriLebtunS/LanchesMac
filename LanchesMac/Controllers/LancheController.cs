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
        ViewData["ListaDeLanches"] = new SelectList(await _context.Lanches.ToListAsync(), "LancheId", "Nome");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Lanche model)
    {
        if (ModelState.IsValid)
    {
        await _context.Lanches.AddAsync(model);
        await _context.SaveChangesAsync();
        return View("_CadastradoComSucesso");
    }

    ViewData["ListaDeLanches"] = new SelectList(await _context.Lanches.ToListAsync(), "LancheId", "Nome");
    return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var lanches = await _context.Lanches.FindAsync(id);

        if (lanches == null)
            return NotFound();

        return View(lanches);
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
