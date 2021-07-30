using Avocado.Base.Interfaces;
using Avocado.Database;
using Avocado.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    public class BaseController : Controller
    {
        public DAL Context => Service.Context;
        public readonly IService Service;

        public BaseController(IService service)
        {
            Service = service;
        }

        protected JsonResult JsonErrorMessage(string message) => Json(new GeneralJsonMessage<string> {Result = "FAIL", Detail = message});
    }
}