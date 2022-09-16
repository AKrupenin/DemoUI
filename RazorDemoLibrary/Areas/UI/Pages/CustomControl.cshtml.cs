using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemoLibrary.Model;
using System.Reflection;

namespace RazorDemoLibrary.Areas.UI.Pages
{
    public class CustomControlModel : LayoutMaster
    {
        public CustomControlModel()
            :base()
        {

        }
        public void OnGet()
        {
            ////
            //string assemblyPath = @"C:\Users\akrupenin\source\repos\CustomControl\CustomControl\bin\Debug\net6.0\CustomControl.dll";
            //string customClass = "CustomControl.TestControl";

            //Assembly assembly = Assembly.LoadFile(assemblyPath);

            //Type type = assembly.GetType(customClass);

            //ICustomControl customControl = (ICustomControl)Activator.CreateInstance(type);

            //customControl.RegisterControl(this);
        }
    }
}
