using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Lvb.Sdk
{
    public class LvbApi
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

        public static bool Delivery(DeliveryFilterModel model, string key)
        {
            string md5str = model.PartnerId + model.Warehouse + model.ExpressType + model.PackageId + model.OutPackageId + model.IsUPU + model.DeliveryTime + model.UserName + key;
            string postUrl = "http://api.cnyto.me/delivery?Sing=" + MD5.GetMD5(md5str);
            bool isTrue = DoPost(postUrl, JsonConvert.SerializeObject(model)).Result;
            if (!isTrue)
            {
                return false;
            }
            return true;
        }
    }
}
