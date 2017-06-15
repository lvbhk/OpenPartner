package com.lvb.sdk;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;



public class program {

	public static void main(String[] args) {
		long timestamp = new Date().getTime();
		String key = "GWYULJ1BS9L1YBDG";
		List<DeliveryFilterModel> list = new ArrayList<DeliveryFilterModel>();
		DeliveryFilterModel model = new DeliveryFilterModel();
		model.setPartnerId(Long.parseLong("832488781855727619"));
		model.setWarehouse("巴黎仓库"); 
		model.setPackageId("1234567890"); 
		model.setOutPackageId("ac123578"); 
		model.setExpressType("法国邮政"); 
		model.setIsUPU(true); 
		model.setDeliveryTime(timestamp + ""); 
		model.setUserName("Jack"); 
		
		List<Product> products = new ArrayList<Product>();
        products.add(new Product("123456", "2"));
        products.add(new Product("654321","2"));
        model.setProducts(products);
        list.add(model);
		
		LvbApi.Delivery(list, key);
		//todo: buessines
	}
	
	

}
