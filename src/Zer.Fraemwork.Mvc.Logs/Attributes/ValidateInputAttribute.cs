using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Zer.Framework.Attributes;
using Zer.Framework.Cache;
using Zer.Framework.Dto;
using Zer.Framework.Exception;
using Zer.Framework.Export.Attributes;
using Zer.Framework.Extensions;
using Zer.Framework.Helpers;

namespace Zer.Framework.Mvc.Logs.Attributes
{
    public class ValidateInputsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.RequestContext.HttpContext.Request.HttpMethod.Equals("GET") ||
                  filterContext.RequestContext.HttpContext.Request.HttpMethod.Equals("POST")))
            {
                throw new CustomException("非法请求");
            }

            //filterContext.RequestContext.HttpContext.Request.UrlReferrer.Host .Contains("")

            if (filterContext.RequestContext.HttpContext.Request.UrlReferrer == null)
            {
                throw new CustomException("非法请求");
            }

            if (filterContext.RequestContext.HttpContext.Request.UrlReferrer.Host.Contains(CacheHelper.GetCache("WebHost").ToString()))
            {
                throw new CustomException("非法请求");
            }

            var attributes = filterContext.ActionDescriptor.GetCustomAttributes(false);

            if (!attributes.Any(x => x is UnValidateInputsAttribute))
            {
                var replaceAttributes = attributes.Where(x => x is ReplaceSpecialCharInParameterAttribute)
                                        .Cast<ReplaceSpecialCharInParameterAttribute>().ToList();

                var getParameterFromSessionAttributeList = attributes.Where(x => x is GetParameteFromSessionAttribute)
                    .Cast<GetParameteFromSessionAttribute>().ToList();

                if (!getParameterFromSessionAttributeList.IsNullOrEmpty())
                {
                    foreach (GetParameteFromSessionAttribute attribute in getParameterFromSessionAttributeList)
                    {
                        var sessionCode = filterContext.ActionParameters[attribute.SessionParameterName].ToString();
                        object obj = null;

                        try
                        {
                            obj = filterContext.HttpContext.Session[sessionCode];
                        }
                        catch
                        {
                            // ignored
                        }

                        if (obj == null)
                        {
                            throw new CustomException("未找到指定值", "参数", attribute.SessionParameterName);
                        }

                        if (!replaceAttributes.IsNullOrEmpty())
                        {
                            foreach (var replaceAttribute in replaceAttributes)
                            {
                                var result = replaceAttribute.ReplaceUnsafeChar(obj);
                                filterContext.HttpContext.Session[attribute.SessionParameterName] = result;
                            }
                        }
                        ValidateHelper.ValidateObjectIsSafe(obj);
                    }
                }

                var inputParameters = filterContext.ActionParameters
                                        .Where(x =>
                                            x.Value is IValidateInputParameter ||
                                            x.Value is string ||
                                            x.Value is IEnumerable<IValidateInputParameter>
                                        )
                                        .ToDictionary(x => x.Key, x => x.Value);

                foreach (var inputParameter in inputParameters)
                {
                    if (!replaceAttributes.IsNullOrEmpty())
                    {
                        foreach (var replaceAttribute in replaceAttributes)
                        {

                            filterContext.ActionParameters[inputParameter.Key] = replaceAttribute.ReplaceUnsafeChar(inputParameter.Value);
                        }
                    }

                    ValidateHelper.ValidateObjectIsSafe(inputParameter.Value);
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}