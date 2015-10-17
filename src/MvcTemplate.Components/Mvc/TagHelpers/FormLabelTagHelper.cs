﻿using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcTemplate.Components.Mvc
{
    [HtmlTargetElement("label", Attributes = "comp-for")]
    public class FormLabelTagHelper : TagHelper
    {
        [HtmlAttributeName("comp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("comp-required")]
        public Boolean? Required { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes["for"] = TagBuilder.CreateSanitizedId(For.Name, "_");
            TagBuilder requiredSpan = new TagBuilder("span");
            requiredSpan.AddCssClass("require");

            if (Required == true)
                requiredSpan.InnerHtml.Append("*");

            if (!Required.HasValue && IsRequiredExpression())
                requiredSpan.InnerHtml.Append("*");

            output.Content.Append(For.ModelExplorer.Metadata.DisplayName);
            output.Content.Append(requiredSpan);
        }

        private Boolean IsRequiredExpression()
        {
            if (For.Metadata.ModelType.IsValueType && Nullable.GetUnderlyingType(For.Metadata.ModelType) == null)
                return true;

            return For.Metadata.ValidatorMetadata.Any(validator => validator is RequiredAttribute);
        }
    }
}
