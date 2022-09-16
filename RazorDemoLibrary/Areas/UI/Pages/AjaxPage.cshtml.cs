using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorDemoLibrary.Areas.UI.Pages
{
    public class AjaxPageModel : LayoutMaster
    {
        public AjaxPageModel()
            :base()
        {

        }

        public void OnGet()
        {
            this.RegisterJS();
        }

        private void RegisterJS()
        {
            RegisterResourceManager.RegisterResource("/AjaxTest.js", "RazorDemoLibrary", this);
        }
    }
}
