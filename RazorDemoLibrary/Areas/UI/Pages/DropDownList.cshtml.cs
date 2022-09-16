using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorDemoLibrary.Model;

namespace RazorDemoLibrary.Areas.UI.Pages
{
    public class DropDownListModel : LayoutMaster
    {
        public DropDownListModel()
            : base()
        {

        }

        public void OnGet()
        {
            BindStaff();
        }

        
        public List<Person> Staff { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedStaffId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SecondSelectedStringValue { get; set; }

        public string DivText { get; set; }

        public string SecondDivText { get; set; }



        public void OnPostClick()
        {
            BindStaff();

            Person? person = this.Staff.FirstOrDefault(p => p.Id == this.SelectedStaffId);
            this.DivText = person?.Name ?? string.Empty;
            this.SecondDivText = string.Empty;


        }

        public IActionResult OnPostSecondClick()
        {
            BindStaff();

            this.SecondDivText = this.SecondSelectedStringValue;
            this.DivText = string.Empty;
            return Page();

        }

        private void BindStaff()
        {
            Staff = new List<Person>
            {
                new Person{ Id = 1, Name = "Mike", Department = "IT"},
                new Person{ Id = 2, Name = "Pete", Department = "Sales"},
                new Person{ Id = 3, Name = "Katy", Department = "Admin"},
                new Person{ Id = 4, Name = "Dean", Department = "Sales"}
            };

        }
    }
}
