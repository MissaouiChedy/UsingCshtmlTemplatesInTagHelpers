using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsingCshtmlTemplatesInTagHelpers.TagHelpers
{
    [HtmlTargetElement("template-renderer-new-viewdata")]
    public class TemplateRendererWithNewViewDataTagHelper : TagHelper
    {

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        private IHtmlHelper _htmlHelper;
        
        private IModelMetadataProvider _modelMetadataProvider;
        /*
         * This constructor requests the injection of a IModelMetadataProvider instance
         */ 
        public TemplateRendererWithNewViewDataTagHelper(IHtmlHelper htmlHelper, 
            IModelMetadataProvider metadataProvider)
        {
            _htmlHelper = htmlHelper;
            _modelMetadataProvider = metadataProvider;
        }

        public override async Task ProcessAsync(TagHelperContext context,
            TagHelperOutput output)
        {
            (_htmlHelper as IViewContextAware).Contextualize(ViewContext);
            // Actual instanciation of the new ViewData Dictionary
            ViewDataDictionary viewData = new ViewDataDictionary(_modelMetadataProvider, new ModelStateDictionary());

            Holder model = new Holder { Name = "Joel" };
            viewData["Name"] = "Jeff";
            
            output.TagName = "div";
            /*
             * model is passed explicitly
             * new ViewData instance needs to be explicitly
             */
            output.Content.SetHtmlContent(
                await _htmlHelper.PartialAsync("TemplateNewViewData", model, viewData));
        }
    }
}
