using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorDemoLibrary
{
    [Route("api/Ajax")]
    public class AjaxController : Controller
    {
        [HttpGet]
        public JsonResult Test()
        {
            return new JsonResult(DateTime.Now);
        }
    }
}
