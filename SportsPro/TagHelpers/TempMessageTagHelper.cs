using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
// Need to add sportspro taghelper in _viewimports.cshtml file

namespace SportsPro.TagHelpers
{
    // taghelper model to display temp messages on the website
    [HtmlTargetElement("TempMessage")]
    public class TempMessageTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var td = ViewCtx.TempData;
            if (td.Keys.Contains("message"))
            { 
                output.TagName = "h3";
                output.TagMode = TagMode.StartTagAndEndTag;

                string newClasses = "bg-secondary text-center text-white p-2";
                string oldClasses = output.Attributes["class"]?.Value?.ToString();
                string classes = (string.IsNullOrEmpty(oldClasses)) ?
                    newClasses : $"{oldClasses} {newClasses}";

                output.Attributes.SetAttribute("class", classes);
                output.Content.SetContent(td["message"].ToString());
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }//TempMessageTagHelper class
}//namespace
