using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemoLibrary.ViewModel;

namespace RazorDemoLibrary.Areas.UI.Pages
{
    public class TestControlPageModel : LayoutMaster
    {
        public TestControlPageModel()
            :base()
        {

        }

        
        public TextBoxControl TextBoxControl { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Value { get; set; }

        public string DivText { get; set; }

        public void OnGet()
        {
        }

        public void OnPostClick()
        {
            this.DivText = this.Value;

            this.TextBoxControl = new TextBoxControl()
            {
                Value = this.Value
            };
        }
    }
}
