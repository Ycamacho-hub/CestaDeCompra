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
                // Al tercer fallo bloquemos el usurio
                if (SessionUtils.GetUserSessionTry(HttpContext, us.Email) > 3)
                {
                    ModelState.AddModelError("Email", "Usuario bloqueado");               
                    userRepo.BlockUsuari(us.Email);
                    return View(us);
                }
                // Avisamos que contraseña es incorrecata y aumentamos un fallo
                ModelState.AddModelError("Password", "Contraseña incorrecta");
                SessionUtils.SetUserSessionTry(HttpContext, us.Email);
             
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
