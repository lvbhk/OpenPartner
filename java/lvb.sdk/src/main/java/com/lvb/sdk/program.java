package com.lvb.sdk;

import java.util.Date;



public class program {

	public static void main(String[] args) {
		long timestamp = new Date().getTime();
		String key = "7SG7VNJRJ2C1R8V3";
		DeliveryFilterModel model = new DeliveryFilterModel();
		model.setPartnerId(Long.parseLong("832488781855727619"));
		model.setWarehouse("巴黎仓库"); 
		model.setPackageId("1234567890"); 
		model.setOutPackageId("ac123578"); 
		model.setExpressType("法国邮政"); 
		model.setIsUPU(true); 
		model.setDeliveryTime(timestamp + ""); 
		model.setUserName("Jack"); 

		LvbApi.Delivery(model, key);
		//todo: buessines
	}
	
	

}
