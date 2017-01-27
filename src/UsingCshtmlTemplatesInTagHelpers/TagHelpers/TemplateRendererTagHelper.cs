using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace UsingCshtmlTemplatesInTagHelpers.TagHelpers
{
    public class Holder
    {
        public string Name { get; set; }
    }

    public class TemplateRendererTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        private IHtmlHelper _htmlHelper;

        public TemplateRendererTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public override async Task ProcessAsync(TagHelperContext context
            , TagHelperOutput output)
        {
            (_htmlHelper as IViewContextAware).Contextualize(ViewContext);
            
            /*
             * Create some data that are going 
             * to be passed to the view
             */
            _htmlHelper.ViewData["Name"] = "Ali";
            _htmlHelper.ViewBag.AnotherName = "Kamel";
            Holder model = new Holder { Name = "Charles Henry" };

            output.TagName = "div";
            /*
             * model is passed explicitly
             * ViewData and ViewBag are passed implicitly
             */
            output.Content.SetHtmlContent(await _htmlHelper.PartialAsync("Template", model));
        }
    }
}
