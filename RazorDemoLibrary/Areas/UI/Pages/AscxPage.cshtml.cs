using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemoLibrary.Model;
using RazorDemoLibrary.ViewModel;

namespace RazorDemoLibrary.Areas.UI.Pages
{
    public class AscxPageModel : LayoutMaster
    {
        public AscxPageModel()
            : base()
        {

        }

        //”казываем, что св-во должно биндитс€ при сабмите, иначе при вызове обработчика св-во будет null
        [BindProperty(SupportsGet = true)]
        public TextBoxControl TextBoxValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public TextBoxControl SecondTextBoxValue { get; set; }

        public string DivText { get; set; }

        public string SecondDivText { get; set; }

        public void OnGet()
        {
            this.TextBoxValue = new TextBoxControl();
            this.SecondTextBoxValue = new TextBoxControl();
        }

        public void OnPostClick()
        {
            this.DivText = this.TextBoxValue.Value;
            this.SecondDivText = string.Empty;
        }

        public void OnPostSecondClick()
        {
            this.DivText = string.Empty;
            this.SecondDivText = this.SecondTextBoxValue.Value;
        }
    }
}
