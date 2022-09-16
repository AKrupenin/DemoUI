using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorDemoLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorDemoLibrary.TagHelpers
{
    [HtmlTargetElement("custom-control")]
    public class CustomControlTagHelper : TagHelper
    {
        public TextBoxControl AspFor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.Add("type", "text");
            output.Attributes.Add("value", this.AspFor?.Value);
        }
    }
}
