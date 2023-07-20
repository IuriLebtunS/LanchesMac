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

    public async Task<IActionResult> Create()
    {
        var categorias = await _context.Categorias.ToListAsync();

        ViewData["ListaDeCategorias"] = new SelectList(categorias, "CategoriaId", "CategoriaNome");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string nome, string descricaoCurta, string descricaoDetalhada, decimal preco,string imagemUrl,string imagemThumbnailUrl,bool isLanchePreferido, bool emEstoque,int categoria)
    {
        var lanche = new Lanche(nome, descricaoCurta,descricaoDetalhada,preco,imagemUrl,imagemThumbnailUrl,isLanchePreferido,emEstoque, categoria);

        await _context.Lanches.AddAsync(lanche);

            await _context.SaveChangesAsync();

            return View("_CadastradoComSucesso");
    }

    public async Task<IActionResult> List()
    {
        var lista = await _context.Lanches.ToListAsync();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}