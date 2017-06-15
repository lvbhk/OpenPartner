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
            string token = "GWYULJ1BS9L1YBDG";
            List<DeliveryFilterModel> list = new List<DeliveryFilterModel>();
            DeliveryFilterModel model = new DeliveryFilterModel();
            model.PartnerId = 832488781855727619;
            model.Warehouse = "巴黎仓库";
            model.PackageId = "862203710082781184";
            model.OutPackageId = "862219532247371776";
            model.ExpressType = "法国邮政";
            model.IsUPU = true;
            model.DeliveryTime = timestamp.ToString();
            model.UserName = "Jack";
            List<Product> products = new List<Product>();
            products.Add(new Product
            {
                ProductId = "123456",
                Quantity = "2"
            });
            products.Add(new Product
            {
                ProductId = "654321",
                Quantity = "2"
            });
            model.Products = products;
            list.Add(model);
            Result r = HttpClintHelper.Delivery(list, token);
            Console.WriteLine(r.Status);
            Console.WriteLine(r.Message);
            Console.ReadKey();
            //todo: buessines
        }
    }
}
