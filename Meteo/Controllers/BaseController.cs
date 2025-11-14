using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Meteo.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {

    }
}
