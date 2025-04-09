using CestaDeCompra.Models;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace CestaDeCompra.Utils
{
    public class SessionUtils
    {
        const string SessionKeyList = "_listaPrductos";
        const string SessionKeyUser = "_user";
        const string SessionKeyTry = "_try";

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
        public static UsuariLogin? GetSessionUsuari(HttpContext htp)
        {
            string? jsonUsuari;
            UsuariLogin? user = null;
            if (htp.Session.GetInt32(SessionKeyUser) != null)
            {
                jsonUsuari = htp.Session.GetString(SessionKeyUser);
                user = JsonSerializer.Deserialize<UsuariLogin>(jsonUsuari);
            }

            return user;
        }

        /// <summary>
        /// Serializa y guarda en la Session el usuario pasado por parámetro
        /// </summary>
        /// <param name="htp"></param>
        /// <param name="user"></param>
        public static void SetSessionUsuari(HttpContext htp, UsuariLogin user)
        {
            string jsonUser = JsonSerializer.Serialize<UsuariLogin>(user);
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

        /// <summary>
        /// Retrona el número de intentos de inicio de sesion fallidos que tenga un usuario
        /// </summary>
        /// <param name="htp"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetUserSessionTry(HttpContext htp, string userId)
        {
            Dictionary<string, int> usersTry;
            int trys = 0;

            if (htp.Session.GetString(SessionKeyTry) != null)
            {
                string jsonTrys = htp.Session.GetString(SessionKeyTry);
                usersTry = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonTrys);

                if(usersTry.ContainsKey(userId)) trys = usersTry[userId];
            }

            return trys;
        }

        /// <summary>
        /// Aumente en una unidad los intentos fallidos de sesión de un usuario
        /// </summary>
        /// <param name="htp"></param>
        /// <param name="userId"></param>
        public static void SetUserSessionTry(HttpContext htp, string userId)
        {
            Dictionary<string, int> usersTry = new();
            int trys = 1;

            if (htp.Session.GetString(SessionKeyTry) != null)
            {
                string jsonTrys = htp.Session.GetString(SessionKeyTry);
                usersTry = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonTrys);

                if (usersTry.ContainsKey(userId)) {
                    trys = usersTry[userId];
                    trys++;
                }
            }

            usersTry[userId] = trys;
            string json = JsonSerializer.Serialize<Dictionary<string, int>>(usersTry);
            htp.Session.SetString(SessionKeyTry, json);
        }

    }
}
