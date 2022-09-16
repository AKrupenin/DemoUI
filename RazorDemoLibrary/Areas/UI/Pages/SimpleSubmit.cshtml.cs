using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorDemoLibrary.Areas.UI.Pages
{
    public class SimpleSubmitModel : LayoutMaster
    {
        public SimpleSubmitModel():
            base()
        {
            
        }

        [BindProperty(SupportsGet = true)]
        public string TextBoxValue { get; set; }

        public string DivText { get; set; }
        

        public void OnGet()
        {
            RegisterResourceManager.RegisterResource("/AjaxTest.js", "RazorDemoLibrary", this);
        }

        /// <summary>
        /// Ìועמה גûחûגאולûי ןנט סאבלטעו ס ץ‎םהכונמל myclick
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostMyClick()
        {
            this.DivText = this.TextBoxValue;
            return Page();
            
        }
    }
}
