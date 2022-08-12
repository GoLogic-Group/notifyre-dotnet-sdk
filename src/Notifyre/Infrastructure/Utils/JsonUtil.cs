using Notifyre.Infrastructure.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO.Compression;
using System.IO;

namespace Notifyre.Infrastructure.Utils
{
    public static class JsonUtil
    {
        public static StringContent CreateBody(object parameters)
        {
            var jsonObj = CreateJson(parameters);
            return new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
        }

        public static HttpContent CompressRequestBody(object parameters)
        {
            var jsonObj = CreateJson(parameters);
            string content = jsonObj.ToString();
            var compressedStream = new MemoryStream();
            using (var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                {
                    contentStream.CopyTo(gzipStream);
                }
            }

            var httpContent = new ByteArrayContent(compressedStream.ToArray());
            return httpContent;
        }

        private static object CreateJson(object parameters)
        {
            var type = parameters.GetType();
            if (
                type == typeof(Guid)
                || type.IsPrimitive
                || type == typeof(decimal)
                || type == typeof(string)
                )
            {
                return new JValue(parameters.ToString());
            }
            else
            {
                JObject jsonObj = new JObject();
                foreach (var prop in parameters.GetType().GetProperties())
                {
                    if (Attribute.IsDefined(prop, typeof(BodyParamAttribute)))
                    {
                        if (Attribute.IsDefined(prop, typeof(DateTimeFormatAttribute)))
                        {
                            var dateTimeFormatAttribute = (DateTimeFormatAttribute)Attribute.GetCustomAttribute(prop, typeof(DateTimeFormatAttribute));
                            if (prop.PropertyType == typeof(DateTime))
                            {
                                var date = (DateTime)prop.GetValue(parameters);
                                var jsonProperty = new JProperty(prop.Name, date.ToString(dateTimeFormatAttribute.Format));
                                jsonObj.Add(jsonProperty);
                            }
                            else
                            {
                                var date = (DateTime?)prop.GetValue(parameters);
                                var jsonProperty = new JProperty(prop.Name, date?.ToString(dateTimeFormatAttribute.Format));
                                jsonObj.Add(jsonProperty);
                            }
                        }
                        else if (prop.PropertyType.IsEnum)
                        {
                            var bodyParamAttribute = (BodyParamAttribute)Attribute.GetCustomAttribute(prop, typeof(BodyParamAttribute));
                            if (bodyParamAttribute.Type != null)
                            {
                                var val = EnumUtil.ToString(bodyParamAttribute.Type, prop.GetValue(parameters).ToString());
                                var jsonProperty = new JProperty(prop.Name, val);
                                jsonObj.Add(jsonProperty);
                            }
                            else
                            {
                                var jsonProperty = new JProperty(prop.Name, prop.GetValue(parameters).ToString());
                                jsonObj.Add(jsonProperty);
                            }
                        }
                        else if (
                            prop.PropertyType.Namespace == "Notifyre"
                            || prop.PropertyType.Namespace == "Notifyre.Services.Fax.FaxSend.SubmitFax.SendFax"
                        )
                        {
                            var jsonProperty = new JProperty(prop.Name, CreateJson(prop.GetValue(parameters)));
                            jsonObj.Add(jsonProperty);
                        }
                        // if we are serialising a collection of custom types or other types
                        else if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            var collection = (IEnumerable)prop.GetValue(parameters, null);
                            if (collection == null)
                            {
                                var jsonProperty = new JProperty(prop.Name, null);
                                jsonObj.Add(jsonProperty);
                            }
                            else
                            {
                                var jsonArray = new JArray();
                                foreach (var item in collection)
                                {
                                    var jsonElement = CreateJson(item);
                                    jsonArray.Add(jsonElement);
                                }
                                jsonObj.Add(prop.Name, jsonArray);
                            }
                        }
                        else if (prop.PropertyType.IsGenericType && prop.PropertyType == typeof(Dictionary<string, string>) && prop.GetValue(parameters) != null)
                        {
                            jsonObj[prop.Name] = JObject.FromObject(prop.GetValue(parameters));
                        }
                        // all other types
                        else
                        {
                            var jsonProperty = new JProperty(prop.Name, prop.GetValue(parameters));
                            jsonObj.Add(jsonProperty);
                        }
                    }
                }
                return jsonObj;
            }
        }
    }
}
