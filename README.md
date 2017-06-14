# 益钱&copy;开放平台接入 OpenPartner API Document

[![N|Solid](https://www.cnyto.me/resources/images/logo.png)](https://partner.cnyto.me)&copy;

开发者根据该文档可是方便接入益钱开放平台，实现客户退税


# 
# 新特性 New Features!
- 2017.03.24
  - 增加说明文档 

# 接入流程
- 1.在益钱注册账号(https://login.cnyto.me/)
- 2.联系益钱开通物流商
- 3.在物流商平台获取开发者key和partnerId(http://partner.cnyto.me/PartnerCenter#/app/logisticsPartner/developer)
- 4.接入退税接口
- 5.设置apiurl（用于益钱退税申请通知和益钱退税单据审核通知）(http://partner.cnyto.me/PartnerCenter#/app/logisticsPartner/developer)
- 测试demo，请先使用测试demo测试成功后再切换至正式环境
  - `https://test.login.cnyto.me/` 用户授权中心
  - `http://test.partner.cnyto.me/`  合作物流商平台
  - `http://test.taxfree.cnyto.me/`   客户退税平台
  - `http://test.api.cnyto.me/`  用户发货api请求地址

## 益钱©API

合作方--->益钱&copy;

### 1. 开始退税
 用户点击按钮跳转到益钱&copy;网站

 - 方法: `GET`
 - URL: `http://taxfree.cnyto.me/UserCenter/Order/create?`
 - eg: `http://taxfree.cnyto.me/UserCenter/Order/create?PartnerId=832488781855727619&Warehouse=巴黎仓库&PackageId=872090330516819968&Sing=bfb4fc2fa60c9fad5f8bb7135c080d1f`
 - Url参数:  
   - PartnerId long 合作伙伴id
   - Warehouse String 系统中配置的仓库
   - PackageId String 仓库中唯一的包裹入库信息ID
   - Sing String   以上参数链接起来然后加上Key(PartnerId + Warehouse + PackageId + Key),然后进行MD5 取32位的hash 编码UTF-8
 
 
- 返回结果:
	用户通过该URL跳转到益钱网站之后将会开始创建退税申请单，如果校验失败，将会提示用户
	
### 2. 商品信息查询接口 
- #### 接口说明
  - 审核通过以后可以通过该接口查询包裹的商品信息，一个入库包裹可能存在多个发票，每个发票存在多个商品

- 方法:`GET`
- URL:`http://api.cnyto.me/product?PackageIds=123,456&PartnerId=832488781855727619&Sing=bfb4fc2fa60c9fad5f8bb7135c080d1f`
- Url参数: 
  - Sing String   PackageIds+PartnerId+key,然后进行MD5 取32位的hash 编码UTF-8
  - PackageIds String  包裹ID集，多个包裹ID以","隔开
  - PartnerId long  合作伙伴id

- 返回结果:
 
```json
    {
	"StatusCode":200,
	"Status":true,
	"Message":"处理成功",
	"Data":[
	    {"PackageId":"123" ,
		"RebateOrders":[
		    { "RebateOrderId":"123",
		        "Products":[
			    {
				"ProductId":"123123123",
				"ProductName":"nike shoses",
				"UnitPrice":"10.00",
				"Quantity":2
			    },
			    {
				"ProductId":"123123123",
				"ProductName":"nike shoses",
				"UnitPrice":"10.00",
				"Quantity":2
			    }
			]
		    },
		    { "RebateOrderId":"456",
			"Products":[
			    {
				"ProductId":"123123123",
				"ProductName":"nike shoses",
				"UnitPrice":"10.00",
				"Quantity":2
			    }
			]
		    }
	    	]
	    },
	    {"PackageId":"packageid2" ,
		"RebateOrders":[
		    {"RebateOrderId":"456",
			"Products":[
			    {
			    	"ProductId":"123123123",
				"ProductName":"addidas shoses",
				"UnitPrice":"10.00",
				"Quantity":2
			    }
			]
		    }
	    	]
	    }
	]
    }
```

  - 返回结果字段定义
    - PackageId 包裹ID
    - RebateOrders 退税清单
    - RebateOrderId 退税单ID
    - Products 发票中的商品清单
    - ProductId 商品ID
    - UnitPrice 商品单价
    - Quantity 商品数量
    - ProductName 商品名称
	
### 3. 用户发货通知 
- #### 接口说明
  - 1 单个包裹发货该数据只有一条，不需要商品信息(ProductsPro字段值为null)
  - 2 合包裹发货，则存在多条数据，PackageId（入库包裹Id）不同，OutPackageId相同，不需要商品信息(ProductsPro字段值为null)
  - 3 拆包发货，则存在多条数据，PackageId（入库包裹Id）相同，OutPackageId不同，同需要提交每个出库包裹的商品信息，商品ID(通过查询接口获取)和数量；如果不上传商品数据，在创建装箱单是需要手动选择出库包裹商品和数量
  - 4 既存在合包又存在拆包时候，则存在多条数据，同需要提交每个出库包裹的商品信息，商品ID(通过查询接口获取)和数量；如果不上传商品数据，在创建装箱单是需要手动选择出库包裹商品和数量

- 方法:`POST`
- 参数:`JSON`
- URL:`http://api.cnyto.me/delivery?`
- eg: `http://api.cnyto.me/delivery?Sing=bfb4fc2fa60c9fad5f8bb7135c080d1f`
- Post Url参数: 
  - Sing String   PostJsonTextContent+key,然后进行MD5 取32位的hash 编码UTF-8
    - 请保证PostJsonTextContent与下面json结构顺序一致
- Post Body:

 ```json
 [
    {
      "PartnerId": "832488781855727619",
      "Warehouse": "巴黎仓库",
      "PackageId": "1234567890",
      "OutPackageId": "12345",
      "ExpressType": "法国邮政",
      "IsUPU": true,
      "DiliveryTime": "1496200166",
      "UserName": "Jack",
      "Products": [
        {
          "ProductId": "123123123",
          "Quantity": 2
        },
        {
          "ProductId": "123123123",
          "Quantity": 2
        }
      ]
    },
    {
      "PartnerId": "832488781855727619",
      "Warehouse": "巴黎仓库",
      "PackageId": "1234567890",
      "OutPackageId": "12345",
      "ExpressType": "法国邮政",
      "IsUPU": true,
      "DiliveryTime": "1496200166",
      "UserName": "Jack",
      "Products": [
        {
	  "ProductId": "123123123",
          "Quantity": 2
        },
        {
	  "ProductId": "123123123",
          "Quantity": 2
        }
      ]
    }
  ]
```

   - 字段定义
     - PartnerId 合作伙伴ID
     - Warehouse 仓库名称
     - ExpressType 快递类型名称
     - PackageId 包裹Id
     - OutPackageId 发往国内的包裹ID
     - Products 包裹中的商品清单
     - ProductId 商品ID
     - Quantity 商品数量
     - IsUPU 是否万国邮联
     - DeliveryTime  发货时间（包裹发往国内的时间）,时间戳
     - UserName 用户名（在合作伙伴系统中的用户名）
- 返回结果:
 
```json
{
	"StatusCode":200,
	"Status":true,
	"Message":"处理成功"
}
```


## 合作方API
益钱&copy;--->合作方
### 1. 退税申请通知
 当用户成功进行了退税申请以后，益钱&copy;将会主动向合作伙伴的接口地址发送一个通知，合作伙伴应当对指定的单据进行标记显示该单据用户申请了退税
	 
- 方法:`POST`
- 参数:`JSON`
- URL:`apiurl/created?`
- Post Url参数:  
   - Sing String   Post Body参数链接起来然后加上Key(PartnerId + Warehouse + PackageId + Key),然后进行MD5 取32位的hash 编码UTF-8
- Post Body:

 ```json
  {
	"Warehouse":"巴黎仓库",
	"PackageId":"1234567890",
	"RebateOrderStatus":10
  }
```
  - RebateOrderStatus
    - 10 Waiting
    - 20 Audited
    - 30 Failed
- 返回结果:

 ```json
{
	"StatusCode":200,
	"Status":true,
	"Message":"处理成功"
}
```


### 2.益钱退税单据审核通知  
 当益钱&copy;对用户提交的退税单据进行审核后，益钱&copy;将会主动向合作伙伴的接口地址发送一个通知

- 方法:`POST`
- 参数:`JSON`
- URL:`apiurl/audited?`
- Post Url参数:  
   - Sing String   Post Body参数链接起来然后加上Key(PartnerId + Warehouse + PackageId + Key),然后进行MD5 取32位的hash 编码UTF-8
- Post Body:

 ```json
  {
	"Warehouse":"巴黎仓库",
	"PackageId":"1234567890",
	"RebateOrderStatus":20,
  }
```

  - RebateOrderStatus
    - 10 Waiting
    - 20 Audited
    - 30 Failed
    
- 返回结果:
 
 ```json
{
	"StatusCode":200,
	"Status":true,
	"Message":"处理成功"
}
```


