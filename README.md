# 益钱&copy;开放平台介入 OpenPartner API Document

[![N|Solid](https://www.cnyto.me/resources/images/logo.png)](https://partner.cnyto.me)&copy;

开发者根据该文档可是方便接入益钱开放平台，实现客户退税


# 
# 新特性 New Features!
- 2017.03.24
  - 增加说明文档 

## 益钱©API

合作方--->益钱&copy;

### 1. 开始退税
 用户点击按钮跳转到益钱&copy;网站

 - 方法: `GET`
 - URL: `taxfree.cnyto.me/UserCenter/Order/create?`
 - 参数:  
   - PartnerId long 合作伙伴id
   - Warehouse String 系统中配置的仓库
   - PackageId String 仓库中唯一的包裹入库信息ID
   - Sing String   以上参数链接起来然后加上Key(PartnerId + Warehouse + PackageId + Key),然后进行MD5 取32位的hash 编码UTF-8
 
 
- 返回结果:
	用户通过该URL跳转到益钱网站之后将会开始创建退税申请单，如果校验失败，将会提示用户
	
### 2. 用户发货通知 
当用户成功进行了发货申请以后，合作方应通知益钱&copy;

- 方法:`POST`
- 参数:`JSON`
- URL:`api.cnyto.me/delivery?`
- Post Url参数:  
  - Sing String   Post Body参数链接起来然后加上Key(PartnerId + Warehouse + ExpressType + PackageId + OutPackageId + IsUPU + DiliveryTime + UserName + Key),然后进行MD5 取32位的hash 编码UTF-8
- Post Body:

 ```json
  {
	"PartnerId":"832488781855727619",
	"Warehouse":"巴黎仓库",
	"ExpressType":"法国邮政",
	"PackageId":"1234567890",
	"OutPackageId":"ac123578",
	"IsUPU":true,
	"DiliveryTime":"1496200166", --TimeSpan
	"UserName":"Jack"	
  }
```

   - 字段定义
     - IsUPU 是否万国邮联
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


