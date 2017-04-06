package lvb.sdk;


public class DeliveryFilterModel {

	public long PartnerId;
	public String Warehouse;
	public String PackageId;
	public String OutPackageId;
	public String ExpressType;
	public boolean IsUPU;
	public String DeliveryTime;

	public String UserName;

	public String getUserName() {
		return UserName;
	}

	public void setUserName(String userName) {
		UserName = userName;
	}
	public long getPartnerId() {
		return PartnerId;
	}

	public void setPartnerId(long partnerId) {
		PartnerId = partnerId;
	}

	public String getWarehouse() {
		return Warehouse;
	}

	public void setWarehouse(String warehouse) {
		Warehouse = warehouse;
	}

	public String getPackageId() {
		return PackageId;
	}

	public void setPackageId(String packageId) {
		PackageId = packageId;
	}

	public String getOutPackageId() {
		return OutPackageId;
	}

	public void setOutPackageId(String outPackageId) {
		OutPackageId = outPackageId;
	}

	public String getExpressType() {
		return ExpressType;
	}

	public void setExpressType(String expressType) {
		ExpressType = expressType;
	}

	public boolean isIsUPU() {
		return IsUPU;
	}

	public void setIsUPU(boolean isUPU) {
		IsUPU = isUPU;
	}

	public String getDeliveryTime() {
		return DeliveryTime;
	}

	public void setDeliveryTime(String deliveryTime) {
		DeliveryTime = deliveryTime;
	}

}
