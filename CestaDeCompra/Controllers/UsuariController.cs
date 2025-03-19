using Microsoft.AspNetCore.Mvc;
using CestaDeCompra.Models;
using CestaDeCompra.Utils;
using CestaDeCompra.Repository.interfaces;

namespace CestaDeCompra.Controllers
{
    public class UsuariController : Controller
    {

        public readonly IUsuariRepository _usuariRepository;

        public UsuariController(IUsuariRepository usuariRepository)
        {
            _usuariRepository = usuariRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuari us)
        {
            if (!_usuariRepository.ExistUsuari(us.Email))
            { 
                ModelState.AddModelError("Email", "Usuario inexistente");
            }
            else if(!_usuariRepository.CheckOutUsuari(us))
            {
                // Al tercer fallo bloquemos el usurio
                if (SessionUtils.GetUserSessionTry(HttpContext, us.Email) > 3)
                {
                    ModelState.AddModelError("Email", "Usuario bloqueado");
                    _usuariRepository.BlockUsuari(us.Email);
                    return View(us);
                }
                // Avisamos que contraseña es incorrecata y aumentamos un fallo
                ModelState.AddModelError("Password", "Contraseña incorrecta");
                SessionUtils.SetUserSessionTry(HttpContext, us.Email);
             
            }
            if (_usuariRepository.CheckOutUsuari(us))
            {
                us = _usuariRepository.GetUsuari(us.Email);
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
