using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorDemoLibrary.Areas.UI.Pages
{
    public class DoubleSubmitModel : LayoutMaster
    {
        public DoubleSubmitModel() :
            base()
        {
        }

        [BindProperty(SupportsGet = true)]
        public string TextBoxValue { get; set; }

        public string DivText { get; set; }


        [BindProperty(SupportsGet = true)]
        public string SecondTextBoxValue { get; set; }

        public string SecondDivText { get; set; }


        public void OnGet()
        {

        }


        public IActionResult OnPostClick()
        {
            this.DivText = this.TextBoxValue;
            this.SecondDivText = string.Empty;
            return Page();

        }

        public IActionResult OnPostSecondClick()
        {
            this.SecondDivText = this.SecondTextBoxValue;
            this.DivText = string.Empty;
            return Page();

        }
    }
}
