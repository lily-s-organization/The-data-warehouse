using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Mooc.Web.UI.Controllers
{
    public class GetContentApiController : ApiController
    {

        #region get video 
        [HttpGet]
        [HttpPost]
        public HttpResponseMessage Video(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("参数为空") };
            }

            string savePath =System.Web.HttpContext.Current.Server.MapPath("~/Images/Video/");
            var source = Path.Combine(savePath, id);
            if (!File.Exists(source))
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("视频文件不存在") };
            }

            string videoFormat = System.IO.Path.GetExtension(source);//".mp4";
            if (!string.IsNullOrEmpty(videoFormat))
                videoFormat = videoFormat.Trim('.');

            var mediaType = MediaTypeHeaderValue.Parse($"video/{videoFormat}");
            var stream = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read);

            if (Request.Headers.Range != null)
            {
                try
                {
                    var partialResponse = Request.CreateResponse(HttpStatusCode.PartialContent);
                    partialResponse.Content = new ByteRangeStreamContent(stream, Request.Headers.Range, mediaType);

                    return partialResponse;
                }
                catch (InvalidByteRangeException invalidByteRangeException)
                {
                    return Request.CreateErrorResponse(invalidByteRangeException);
                }
            }
            else
            {
                var fullResponse = Request.CreateResponse(HttpStatusCode.OK);

                fullResponse.Content = new StreamContent(stream);
                fullResponse.Content.Headers.ContentType = mediaType;
                return fullResponse;
            }

        }
        #endregion

    }
}
