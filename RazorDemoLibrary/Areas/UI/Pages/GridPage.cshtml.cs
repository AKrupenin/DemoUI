using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemoLibrary.Model;

namespace RazorDemoLibrary.Areas.UI.Pages
{
    public class GridPageModel : LayoutMaster
    {

        public GridPageModel()
            :base()
        {
            
        }

        [BindProperty(SupportsGet = true)]
        public Person Person { get; set; }

        public void OnGet()
        {
            Person = new Person()
            {
                Name = "asd"
            };
        }
    }
}
