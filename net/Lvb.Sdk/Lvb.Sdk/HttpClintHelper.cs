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
    public class HttpClintHelper
    {
        private static async Task<Result> DoPost(string callbackUrl, string json)
        {
            Result result = new Result() { Status = false };
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
                    //response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        Task<string> resultStr = response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<Result>(resultStr.Result);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    //todo: Exception
                }
            }
            return result;
        }

        public static Result Delivery(DeliveryFilterModel model, string token)
        {
            string md5str = model.PartnerId + model.Warehouse + model.ExpressType + model.PackageId + model.OutPackageId + model.IsUPU.ToString().ToLower() + model.DeliveryTime + model.UserName + token;
            string postUrl = "http://api.lvb.com:8080/delivery?Sing=" + MD5.GetMD5(md5str);
            Result result = DoPost(postUrl, JsonConvert.SerializeObject(model)).Result;
            return result;
        }
        
    }
}
