using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LVB_SDK_doNET
{
    public class HttpClintHelper
    {
        private static async Task<bool> DoPost(string callbackUrl, string json)
        {
            var handler = new HttpClientHandler();
            using (var http = new HttpClient(handler))
            {
                try
                {
                    var content = new StringContent(json);
                    content.Headers.ContentType.MediaType = "application/json";
                    content.Headers.ContentType.CharSet = "utf-8";
                    //await异步等待回应
                    var response = await http.PostAsync(callbackUrl, content);
                    //确保HTTP成功状态值
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    //todo: Exception
                }
            }
            return false;
        }

        public static bool Delivery(DeliveryFilterModel model, string token)
        {
            string md5str = model.PartnerId + model.Warehouse + model.ExpressType + model.PackageId + model.OutPackageId + model.IsUPU + model.DeliveryTime + model.UserName + token;
            string postUrl = "http://api.lvb.com:8080/delivery?Sing=" + MD5.GetMD5(md5str);
            bool isTrue = DoPost(postUrl, JsonConvert.SerializeObject(model)).Result;
            if (!isTrue)
            {
                return false;
            }
            return true;
        }
    }
}
