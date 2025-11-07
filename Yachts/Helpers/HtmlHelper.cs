// Helpers/HtmlHelpers.cs
using System;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Yachts.Helpers
{
    public static class HtmlHelpers
    {
        // 必填欄位後面加*
        public static MvcHtmlString LabelForRequired<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var labelText = metadata.DisplayName ?? metadata.PropertyName;

            // 檢查是否必填
            var memberExpression = expression.Body as MemberExpression;
            var propertyInfo = memberExpression?.Member as System.Reflection.PropertyInfo;
            var isRequired = propertyInfo?.GetCustomAttributes(typeof(RequiredAttribute), true).Any() ?? false;

            if (isRequired)
            {
                labelText += " <span class='text-danger'>*</span>";
            }

            var label = new TagBuilder("label");
            label.InnerHtml = labelText;
            label.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression)));

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                label.MergeAttributes(attributes);
            }

            return MvcHtmlString.Create(label.ToString());
        }

        // 資料空的話顯示 尚未輸入資料
        public static MvcHtmlString DisplayOrDefault(this HtmlHelper html, string value, string defaultText = "尚未輸入資料")
        {
            return new MvcHtmlString(!string.IsNullOrEmpty(value) ? value : defaultText);
        }
    }
}