using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemoLibrary.Model;
using RazorDemoLibrary.ModelBind;
using RazorDemoLibrary.ViewModel;

namespace RazorDemoLibrary.Areas.UI.Pages
{
    public class FieldsModel : LayoutMaster
    {
        public FieldsModel()
            :base()
        {

        }

        [BindProperty]
        public List<IFieldControl> Fields { get; set; }

        public void OnGet()
        {
            this.InitFields();
        }

        public void OnPostClick()
        {
            foreach (IFieldControl control in this.Fields)
            {
                control.OnCLick();
            }
        }

        private void InitFields()
        {

            Fields = new List<IFieldControl>()
            {
                new TextBoxControl()
                {
                    Value = "1",
                    Name = "Кастом поле"
                },

                new TextBoxControl()
                {
                    Value = "2",
                    Name = "тестовое поле 1"
                },

                new BoolControl()
                {
                   Name = "Логическое поле",
                   Value = false
                },
            };
        }
    }
}
