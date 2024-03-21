using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mission11_Forbush.Models.ViewModels;

namespace Mission11_Forbush.Infrastructure

    // This is going to build an instance of the PaginationTagHelper and then it's going to build the a tags
    // that we are going to need

{
    [HtmlTargetElement("div", Attributes="page-model")]
    public class PaginationTagHelper : TagHelper
    {
        // Private instance of IUrlHelpFactory (Look up)
        private IUrlHelperFactory urlHelperFactory;

        public PaginationTagHelper (IUrlHelperFactory temp)
        {
            urlHelperFactory = temp;
        }

        // When this is called its going to give us some information about that view context.
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }
        public string? PageAction { get; set; }
        public PaginationInfo PageModel { get; set; }

        // Below are options that we can include in our Tag helper in index
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; } = String.Empty;
        public string PageClassNormal { get; set; } = String.Empty;
        public string PageClassSelected { get; set; } = String.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Checks to see if ViewContext has information and the PageModel is not null (Hover over to see information)
            if (ViewContext != null && PageModel != null)
            {
                // Building a new urlHelper (Look up)
                IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

                TagBuilder result = new TagBuilder("div");

                // This loop pretty much building an <a> tag with a href
                for (int i = 1; i <= PageModel.TotalNumPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = i });

                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }
                    tag.InnerHtml.Append(i.ToString());

                    // It's then going to append the result to <div> tags
                    result.InnerHtml.AppendHtml(tag);
                }

                // Go to the output of this tag which is the <divs> and <a> tags and goes to the Index.cshtml
                // and replaces the < div page-model> lines that we deleted from earlier.
                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
