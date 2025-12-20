using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BerberYonetimSistemi.Models;

namespace BerberYonetimSistemi.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsAuthorized(params Rol[] authorizedRoles)
        {
            var userRole = (Rol)Enum.Parse(typeof(Rol), HttpContext.Session.GetString("Rol") ?? "Kullanici");
            return authorizedRoles.Contains(userRole) || userRole == Rol.Admin;
        }
    }
}

