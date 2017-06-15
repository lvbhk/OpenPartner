package com.lvb.sdk;


public class Product {
	public String ProductId;

	public String Quantity;

	public String getProductId() {
		return ProductId;
	}

	public void setProductId(String productId) {
		ProductId = productId;
	}

	public String getQuantity() {
		return Quantity;
	}

	public void setQuantity(String quantity) {
		Quantity = quantity;
	}
	
	public Product(String ProductId,String Quantity) {
		this.ProductId = ProductId;
		this.Quantity = Quantity;
	}
}
