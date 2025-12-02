using Microsoft.AspNetCore.Mvc;

namespace MyBackend.Controllers;

/// <summary>
/// Controller for serving HTML views (not API endpoints)
/// </summary>
public class HomeController : Controller
{
    // GET: /Home or /
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Home/Products
    public IActionResult Products()
    {
        return View();
    }

    // GET: /Home/Users
    public IActionResult Users()
    {
        return View();
    }

    // GET: /Home/Purchases
    public IActionResult Purchases()
    {
        return View();
    }
}

