using LanchesMac.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using LanchesMac.ViewModels;

public class LancheController : Controller
{
    private readonly AppDbContext _context;

    public LancheController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> List()
    {
        //var lanches = await _context.Lanches.ToListAsync();
        //return View(lanches);
        var LancheListViewModel = new LancheListViewModel();
        LancheListViewModel.Lanches = await _context.Lanches.ToListAsync();
        LancheListViewModel.CategoriaAtual = "Categoria Atual";

        return View(LancheListViewModel);

    }


    public async Task<IActionResult> Create()
    {
        ViewData["listaDeCategorias"] = new SelectList(await _context.Categorias.ToListAsync(), "CategoriaId", "CategoriaNome");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Lanche model)
    {
        if (ModelState.IsValid)
        {
            await _context.Lanches.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", "Lanche");
        }

        ViewData["listaDeCategorias"] = new SelectList(await _context.Categorias.ToListAsync(), "CategoriaId", "CategoriaNome");
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {

        var lanches = await _context.Lanches.FindAsync(id);

        if (lanches == null)
            return NotFound();

        ViewData["listaDeCategorias"] = new SelectList(await _context.Categorias.ToListAsync(), "CategoriaId", "CategoriaNome");
        return View(lanches);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Lanche model)
    {
        if (ModelState.IsValid)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }
        ViewData["listaDeCategorias"] = new SelectList(await _context.Categorias.ToListAsync(), "CategoriaId", "CategoriaNome");
        return View();
    }
    public async Task<IActionResult> Delete(int id)
    {
        var lanches = await _context.Lanches.FirstOrDefaultAsync(m => m.LancheId == id);

        if (lanches == null)
            return NotFound();

        return View(lanches);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(Lanche model)
    {

        _context.Lanches.Remove(model);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(List));
    }

}
