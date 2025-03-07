using CestaDeCompra.Models;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace CestaDeCompra.Utils
{
    public class SessionUtils
    {
        const string SessionKeyBuy = "_compra";
        const string SessionKeyList = "_listaPrductos";
        const string SessionKeyUser = "_user";

        /// <summary>
        ///  Retorna el objeto Cesta que está en la Session
        ///  Si hay ninguna retorna un intancia de Cesta vacia
        /// </summary>
        /// <param name="htp"></param>
        /// <returns>
        /// </returns>
        public static Cesta? GetSessionCesta(HttpContext htp)
        {
            Cesta? cestaProductos = new();

            if (htp.Session.GetInt32(SessionKeyList) != null)
            {
                string jsonCesta = htp.Session.GetString(SessionKeyList);
                cestaProductos = JsonSerializer.Deserialize<Cesta>(jsonCesta);
            }

            return cestaProductos;
        }

        /// <summary>
        ///  Recibe un objeto Cesta y lo guarda en la Session
        /// </summary>
        /// <param name="htp"></param>
        /// <param name="cesta"></param>
        public static void SetSessionCesta(HttpContext htp, Cesta cesta)
        {
            string jsonCesta = JsonSerializer.Serialize<Cesta>(cesta);
            htp.Session.SetString(SessionKeyList, jsonCesta);
        }

        /// <summary>
        /// Elimina la cesta de la session
        /// </summary>
        /// <param name="htp"></param>
        public static void DeleteSessionCesta(HttpContext htp)
        {
            htp.Session.Remove(SessionKeyList);
        }

        /// <summary>
        /// Hace uso de la función 'GetSessionCesta()' para obtener la Cesta.
        /// Luego con ayuda de la función GetProductosTotales() del objeto Cesta 
        /// obtiene y retorna la cantidad de productos reservados por el cliente.
        /// </summary>
        /// <param name="htp"></param>
        /// <returns></returns>
        public static int? GetSessionCestaCantidad(HttpContext htp)
        {
            int? productosTotales = null;

            if (GetSessionCesta(htp) != null) { 
                Cesta cesta = GetSessionCesta(htp);
                productosTotales = cesta.GetProductosTotales();
            }

            return productosTotales;
        }

        /// <summary>
        /// Retorna el usuario que está logeado
        /// </summary>
        /// <param name="htp"></param>
        /// <returns></returns>
        public static Usuari? GetSessionUsuari(HttpContext htp)
        {
            string? jsonUsuari;
            Usuari? user = null;
            if (htp.Session.GetInt32(SessionKeyUser) != null)
            {
                jsonUsuari = htp.Session.GetString(SessionKeyUser);
                user = JsonSerializer.Deserialize<Usuari>(jsonUsuari);
            }

            return user;
        }

        /// <summary>
        /// Serializa y guarda en la Session el usuario pasado por parámetro
        /// </summary>
        /// <param name="htp"></param>
        /// <param name="user"></param>
        public static void SetSessionUsuari(HttpContext htp, Usuari user)
        {
            string jsonUser = JsonSerializer.Serialize<Usuari>(user);
            htp.Session.SetString(SessionKeyUser, jsonUser);
        }

        /// <summary>
        /// Elimina el usuario de la session
        /// </summary>
        /// <param name="htp"></param>
        public static void DeteleSessionUsuari(HttpContext htp)
        {
            htp.Session.Remove(SessionKeyUser);
        }

    }
}
