using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorDemoLibrary.Areas.UI.Pages
{
    public class GetServerTimeHandlerModel : PageModel
    {

        public JsonResult OnGet()
        {
            return new JsonResult(DateTime.Now);
        }
    }
}
