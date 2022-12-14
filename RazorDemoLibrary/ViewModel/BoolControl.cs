using Microsoft.AspNetCore.Mvc;
using RazorDemoLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorDemoLibrary.ViewModel
{
    [BindProperties]
    public class BoolControl : IFieldControl
    {

        public bool Value { get; set; }
        public string Name { get; set; }

        public string Text { get; set; }

        public string TypeName
        {
            get => this.GetType().FullName;
            set { }
        }

        public void OnCLick()
        {
            this.Text = Value.ToString();
        }
    }
}
