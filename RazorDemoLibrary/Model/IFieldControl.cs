using Microsoft.AspNetCore.Mvc;
using RazorDemoLibrary.ModelBind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorDemoLibrary.Model
{
    
    public interface IFieldControl
    {
        string TypeName { get; set; }
        void OnCLick();
    }
}
