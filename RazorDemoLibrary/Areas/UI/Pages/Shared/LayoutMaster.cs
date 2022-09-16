using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorDemoLibrary.Areas.UI
{
    public class LayoutMaster : PageModel
    {
        public LayoutMaster()
        {
            this.PageTitle = "Страница тестового примера";
            
            this.LeftTitle = "Глобальные настройки системы";
            this.HplCurrentUserTitle = "Unknown user";
            this.HplCurrentUserURL = "/_layouts/WSS/DBF/UI/SignOut.aspx";

            this.RegisterUIStyles();
            this.RegisterJQueryCore();
        }

        public string PageTitle { get; set; }

        public string RightTitle { get; set; }

        public string LeftTitle { get; set; }

        #region hplCurrentUser
        public string HplCurrentUserURL { get; protected set; }

        public string HplCurrentUserTitle { get; protected set; }
        #endregion

        public List<string> ScriptResources { get; set; } = new List<string>();

        public List<string> StyleResources { get; set; } = new List<string>();

        private void RegisterUIStyles()
        {
            RegisterResourceManager.RegisterResource("/UI/UI.css", "RazorDemoLibrary", this);
        }

        internal void RegisterJQueryCore()
        {
            string jqueryVersion = "jquery-2.1.3";

            string scriptLink = string.Format("/UI/{0}.min.js", jqueryVersion);
            RegisterResourceManager.RegisterResource(scriptLink, "RazorDemoLibrary", this);
        }
    }
}
