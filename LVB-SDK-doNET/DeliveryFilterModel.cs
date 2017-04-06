namespace LVB_SDK_doNET
{
    public class DeliveryFilterModel
    {
        public long PartnerId { get; set; }
        public string Warehouse { get; set; }
        public string PackageId { get; set; }
        public string OutPackageId { get; set; }
        public string ExpressType { get; set; }
        public bool IsUPU { get; set; }
        public string DeliveryTime { get; set; }
        public string UserName { get; set; }
    }
}