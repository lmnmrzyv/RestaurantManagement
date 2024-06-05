using Microsoft.AspNetCore.Mvc;
using RestaurantManagementWebProject.Models;
using System.Diagnostics;

namespace RestaurantManagementWebProject.Controllers
{
    public class HomeController : Controller
    {
       public IActionResult Index()
       {
            return Content("Hello,World!");
       }
       public IActionResult OtherIndex() 
        {
            return Content("Hello,World 2!");
        }
    }
}