using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Middleware.Core
{
    public static class HttpContextExtensions
    {
        public static string GetHeader(this HttpContext httpContext, string key)
        {
            httpContext?.Request.Headers.TryGetValue(key, out var values);
            return !string.IsNullOrWhiteSpace(values) ? values[0] : "";
        }
    }

    public class WebApiResultMiddleware : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.HttpContext.Request.Path.HasValue)
            {
                var apiName = context.HttpContext.Request.Path.Value.ToLower();
                if (!(apiName.IndexOf("/inside.", StringComparison.Ordinal) > -1 ||
                      apiName.IndexOf("/health", StringComparison.Ordinal) > -1 ||
                      apiName.IndexOf("/open.", StringComparison.Ordinal) > -1))
                {
                    if (context.Result is FileContentResult || context.Result is EmptyResult)
                    {
                        return;
                    }

                    if (context.Result is ObjectResult objectResult)
                    {
                        var settings = new JsonSerializerSettings()
                        {
                            //ContractResolver = new NullToEmptyStringResolver(),
                            DateFormatString = "yyyy-MM-dd HH:mm",
                            ContractResolver = new DefaultContractResolver()
                            {
                                NamingStrategy = new SnakeCaseNamingStrategy()
                            }
                        };
                        context.Result = new JsonResult(new {data = objectResult.Value}, settings);
                    }
                }
            }
        }
    }

    public class NullToEmptyStringResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties()
                .Select(p =>
                {
                    var jp = base.CreateProperty(p, memberSerialization);
                    jp.ValueProvider = new NullToEmptyStringValueProvider(p);
                    return jp;
                }).ToList();
        }
    }

    public class NullToEmptyStringValueProvider : IValueProvider
    {
        readonly PropertyInfo _MemberInfo;

        public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
        {
            _MemberInfo = memberInfo;
        }

        public object GetValue(object target)
        {
            var result = _MemberInfo.GetValue(target) ?? "";
            return result;
        }

        public void SetValue(object target, object value)
        {
            _MemberInfo.SetValue(target, value);
        }
    }
}