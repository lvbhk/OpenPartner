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
            string token = "22VYWU1CAJJMD7O7";
            DeliveryFilterModel model = new DeliveryFilterModel();
            model.PartnerId = 832488781855727619;
            model.Warehouse = "巴黎仓库";
            model.PackageId = "862203710082781184";
            model.OutPackageId = "862219532247371776";
            model.ExpressType = "法国邮政";
            model.IsUPU = true;
            model.DeliveryTime = timestamp.ToString();
            model.UserName = "Jack";
            Result r = HttpClintHelper.Delivery(model, token);
            Console.WriteLine(r.Status);
            Console.WriteLine(r.Message);
            Console.ReadKey();
            //todo: buessines
        }
    }
}
