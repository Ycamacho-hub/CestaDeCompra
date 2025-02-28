using Microsoft.AspNetCore.Mvc;
using CestaDeCompra.Models;
using CestaDeCompra.Repository;

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
            
            //Usuari us = new(Password, Email);
            // Si el usuario existe
            if (userRepo.ExistUsuari(us.Email))
            {
                // Y la contraseña es correcta
                if (us.Password.Equals(userRepo.GetPasswUsuari(us.Email)))
                {
                    //Usuaris.CreateUsuari(us);
                    ViewData["Login"] = "True";
                    ViewData["Usuari"] = us.Email;
                    
                    return LocalRedirect("/producto/index");
                }
                    
            }

            return View(us);
        }

    }
}
