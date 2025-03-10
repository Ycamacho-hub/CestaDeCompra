using Microsoft.AspNetCore.Mvc;
using CestaDeCompra.Models;
using CestaDeCompra.Repository;
using CestaDeCompra.Utils;

namespace CestaDeCompra.Controllers
{
    public class UsuariController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuari us)
        {
            UsuariRepository userRepo = new();

            if (!userRepo.ExistUsuari(us.Email))
            { 
                ModelState.AddModelError("Email", "Usuario inexistente");
            }
            else if(!userRepo.CheckOutUsuari(us))
            {
                ModelState.AddModelError("Password", "Contraseña incorrecta");
            }
            if (userRepo.CheckOutUsuari(us))
            {
                us = userRepo.GetUsuari(us.Email);
                SessionUtils.SetSessionUsuari(HttpContext, us);
                return LocalRedirect("/home/index");
            }
                

            return View(us);
        }

        public IActionResult Logout()
        {
            SessionUtils.DeteleSessionUsuari(HttpContext);
            return LocalRedirect("/home/index");
        }

    }
}
