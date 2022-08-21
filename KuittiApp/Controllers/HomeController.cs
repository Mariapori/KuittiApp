using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KuittiApp.Models;
using KuittiApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;

namespace KuittiApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            var kuitit = _db.Kuitit.Where(o => o.Kayttaja == User.Identity.Name).ToList();
            ViewBag.kuitit = kuitit;
        }

        return View();
    }

    public IActionResult Uusi()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Uusi(string Kuvaus, IFormFile Kuva)
    {
        try
        {
            var kuitti = new Kuitti();
            kuitti.Id = Guid.NewGuid().ToString();
            kuitti.PVM = DateTime.Now;
            kuitti.Kayttaja = User.Identity.Name;
            kuitti.Kuvaus = Kuvaus;

            if(Kuva.Length > 0)
            {
                //TODO: Kuvan kryptaus
                var stream = new MemoryStream();
                Kuva.CopyTo(stream);
                kuitti.Kuva = stream.ToArray();
                kuitti.ContentType = Kuva.ContentType;
                _db.Kuitit.Add(kuitti);
                _db.SaveChanges();
            }

            
        }catch(Exception ex)
        {
            throw;
        }
        return RedirectToAction("Index");
    }

    public IActionResult AvaaKuitti(string Id)
    {
        var kuva = _db.Kuitit.FirstOrDefault(o => o.Id == Id);
        return File(kuva?.Kuva,kuva?.ContentType);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

