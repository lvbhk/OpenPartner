using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvb.Sdk
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long timestamp = Convert.ToInt64(ts.TotalSeconds);
            string key = "7SG7VNJRJ2C1R8V3";
            DeliveryFilterModel model = new DeliveryFilterModel();
            model.PartnerId = 832488781855727619;
            model.Warehouse ="巴黎仓库";
            model.PackageId ="1234567890";
            model.OutPackageId = "ac123578";
            model.ExpressType = "法国邮政";
            model.IsUPU = true;
            model.DeliveryTime = timestamp.ToString();
            model.UserName = "Jack";
            
            Console.WriteLine(LvbApi.Delivery(model, key));
            Console.ReadKey();
            //todo: buessines
        }
    }
}
