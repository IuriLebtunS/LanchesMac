using LanchesMac.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

public class LancheController : Controller
{
    private readonly AppDbContext _context;

    public LancheController(AppDbContext context)
    {
        _context = context;
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