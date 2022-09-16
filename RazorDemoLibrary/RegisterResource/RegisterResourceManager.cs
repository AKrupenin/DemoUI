using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemoLibrary.Areas.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorDemoLibrary
{
    public class RegisterResourceManager
    {
        public static void RegisterResource(string relativeURL, string moduleName, PageModel pageModel)
        {
            if (pageModel is LayoutMaster masterPage)
            {
                string ext = System.IO.Path.GetExtension(relativeURL).ToLower().Trim('.');

                switch (ext)
                {
                    case "js":
                        masterPage.ScriptResources.Add($"~/_content/{moduleName}/{relativeURL}");
                        break;
                    case "css":
                        masterPage.StyleResources.Add($"~/_content/{moduleName}/{relativeURL}");
                        break;
                    default:
                        break;
                }
            }
        }

        public static void RegisterResource(string relativeURL, PageModel pageModel)
        {
            if (pageModel is LayoutMaster masterPage)
            {
                string ext = System.IO.Path.GetExtension(relativeURL).ToLower().Trim('.');

                switch (ext)
                {
                    case "js":
                        masterPage.ScriptResources.Add($"/{relativeURL}");
                        break;
                    case "css":
                        masterPage.StyleResources.Add($"/{relativeURL}");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
