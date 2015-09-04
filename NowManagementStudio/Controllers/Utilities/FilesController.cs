using Newtonsoft.Json;
using NowManagementStudio.Models.UploadFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using NowManagementStudio.Models.Inventory;


namespace NowManagementStudio.Controllers.Utilities
{
    [RoutePrefix("api/Files")]
    public class FilesController : ApiController
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Route("UploadInventory/{id}")]
        [HttpPost] // This is from System.Web.Http, and not from System.Web.Mvc
        public async Task<HttpResponseMessage> Upload(String id)
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = GetMultipartProvider(id);
                var result = await Request.Content.ReadAsMultipartAsync(provider);
                var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
                //Map image to lot
                InventoryContext inventoryDB = new InventoryContext();
                inventoryDB.InsertImageMapping(id, uploadedFileInfo.Name);

            }
            catch (Exception ex)
            {
                log.Error("Error Uploading File : " + ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private CustomMultipartFormDataStreamProvider GetMultipartProvider(String id)
        {
            var uploadFolder = "~/Images/Inventory/FileUploads/"+id+"/"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new CustomMultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private String GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                    return unescapedFormData;
            }

            return null;
        }



    }
}