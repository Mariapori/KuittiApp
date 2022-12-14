using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KuittiApp.Models;
using KuittiApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using PagedList;

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

    public IActionResult Index(int? pageNumber = 1)
    {
        if (User.Identity.IsAuthenticated)
        {
            var kuitit = _db.Kuitit.Where(o => o.Kayttaja == User.Identity.Name).OrderByDescending(o => o.OstoPVM).ToPagedList(pageNumber ?? 1,10);
            ViewBag.kuitit = kuitit;
        }

        return View();
    }

    public IActionResult Uusi()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Uusi(string Kuvaus, DateTime OstoPVM,int TakuuKK, IFormFile Kuva)
    {
        try
        {
            var kuitti = new Kuitti();
            kuitti.Id = Guid.NewGuid().ToString();
            kuitti.OstoPVM = OstoPVM;
            kuitti.Kayttaja = User.Identity.Name;
            kuitti.TakuuKK = TakuuKK;
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

    [Authorize]
    public IActionResult PoistaKuitti(string Id)
    {
        var kuitti = _db.Kuitit.FirstOrDefault(o => o.Id == Id);
        if (kuitti.Kayttaja == User.Identity.Name)
        {
            _db.Kuitit.Remove(kuitti);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            throw new UnauthorizedAccessException("Sinulla ei ole oikeutta poistaa tätä.");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

