using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Web.Mvc;

namespace NowManagementStudio.Helpers
{
    public class JsonController : Controller
    {
        //
        // GET: /Json/
        public new ActionResult Json(object data, JsonRequestBehavior behavior)
        {
            var jsonSerializerSetting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            if (Request.RequestType == WebRequestMethods.Http.Get && behavior == JsonRequestBehavior.DenyGet)
            {
                throw new InvalidOperationException("GET is not permitted for this request.");
            }
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(data, jsonSerializerSetting),
                ContentType = "application/json"
            };
            return jsonResult;
            
        }
	}
}